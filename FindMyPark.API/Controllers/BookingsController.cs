using FindMyPark.API.Data;
using FindMyPark.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindMyPark.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly FindMyPark.API.Services.IPricingService _pricingService;

        public BookingsController(AppDbContext context, FindMyPark.API.Services.IPricingService pricingService)
        {
            _context = context;
            _pricingService = pricingService;
        }

        [HttpPost]
        public async Task<ActionResult<Booking>> CreateBooking(CreateBookingDto dto)
        {
            var listing = await _context.Listings.FindAsync(dto.ListingId);
            if (listing == null) return NotFound("Listing not found");

            var durationHours = (dto.EndTime - dto.StartTime).TotalHours;
            if (durationHours <= 0) return BadRequest("End time must be after start time");

            // Calculate Dynamic Price
            var totalPrice = await _pricingService.CalculatePriceAsync(listing.PricePerHour, dto.ListingId, dto.StartTime, dto.EndTime);

            // APPLY PROMO CODE
            if (!string.IsNullOrEmpty(dto.PromoCode))
            {
                var promo = await _context.PromoCodes.FirstOrDefaultAsync(p => p.Code == dto.PromoCode && p.IsActive);
                if (promo != null && promo.UsageCount < promo.UsageLimit && promo.ExpiryDate > DateTime.UtcNow)
                {
                    var discount = (totalPrice * promo.DiscountPercentage) / 100;
                    if (discount > promo.MaxDiscountAmount) discount = promo.MaxDiscountAmount;
                    
                    totalPrice -= discount;
                    promo.UsageCount++; // Increment usage
                    _context.PromoCodes.Update(promo);
                }
            }

            // WALLET CHECK
            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == dto.DriverId);
            if (wallet == null || wallet.Balance < totalPrice)
            {
                return BadRequest("Insufficient funds in wallet. Please add money.");
            }

            // DEDUCT FUNDS
            wallet.Balance -= totalPrice;
            var transaction = new Transaction
            {
                WalletId = wallet.Id,
                Amount = totalPrice,
                Type = "Debit",
                Description = $"Booking for {listing.Title}"
            };
            
            var booking = new Booking
            {
                ListingId = dto.ListingId,
                DriverId = dto.DriverId,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                TotalPrice = totalPrice,
                Status = "Confirmed"
            };

            var notification = new Notification
            {
                UserId = dto.DriverId,
                Title = "Booking Confirmed",
                Message = $"Your booking for {listing.Title} is confirmed!",
                IsRead = false
            };

            _context.Wallets.Update(wallet);
            _context.Transactions.Add(transaction);
            _context.Bookings.Add(booking);
            _context.Notifications.Add(notification);
            
            // GAMIFICATION: Award Points
            var user = await _context.Users.FindAsync(dto.DriverId);
            if (user != null)
            {
                user.Points += 10;
                _context.Users.Update(user);
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMyBookings), new { userId = booking.DriverId }, booking);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetMyBookings(int userId)
        {
            return await _context.Bookings
                .Include(b => b.Listing)
                .Where(b => b.DriverId == userId)
                .OrderByDescending(b => b.StartTime)
                .ToListAsync();
        }

        [HttpGet("calculate-price")]
        public async Task<ActionResult<object>> CalculatePrice(int listingId, DateTime start, DateTime end)
        {
            var listing = await _context.Listings.FindAsync(listingId);
            if (listing == null) return NotFound();

            var totalPrice = await _pricingService.CalculatePriceAsync(listing.PricePerHour, listingId, start, end);
            return Ok(new { TotalPrice = totalPrice, BaseRate = listing.PricePerHour });
        }

        [HttpGet("last-parked/{userId}")]
        public async Task<ActionResult<object>> GetLastParked(int userId)
        {
            var booking = await _context.Bookings
                .Include(b => b.Listing)
                .Where(b => b.DriverId == userId && b.Status == "Confirmed")
                .OrderByDescending(b => b.EndTime)
                .FirstOrDefaultAsync();

            if (booking == null) return NotFound("No parking history found.");

            return Ok(new
            {
                ListingTitle = booking.Listing.Title,
                Address = booking.Listing.Address,
                Latitude = booking.Listing.Latitude,
                Longitude = booking.Listing.Longitude,
                EndTime = booking.EndTime,
                IsActive = booking.EndTime > DateTime.UtcNow
            });
        }
        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var booking = await _context.Bookings.Include(b => b.Listing).FirstOrDefaultAsync(b => b.Id == id);
            if (booking == null) return NotFound();

            if (booking.Status == "Cancelled") return BadRequest("Booking already cancelled.");
            
            // Refund Logic
            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == booking.DriverId);
            if (wallet != null)
            {
                wallet.Balance += booking.TotalPrice;
                var transaction = new Transaction
                {
                    WalletId = wallet.Id,
                    Amount = booking.TotalPrice,
                    Type = "Credit",
                    Description = $"Refund for {booking.Listing.Title}"
                };
                _context.Wallets.Update(wallet);
                _context.Transactions.Add(transaction);
            }

            booking.Status = "Cancelled";
            await _context.SaveChangesAsync();

            return Ok(new { message = "Booking cancelled and refunded." });
        }

        [HttpPost("{id}/extend")]
        public async Task<IActionResult> ExtendBooking(int id, [FromBody] ExtendBookingDto dto)
        {
            var booking = await _context.Bookings.Include(b => b.Listing).FirstOrDefaultAsync(b => b.Id == id);
            if (booking == null) return NotFound("Booking not found");

            if (booking.Status != "Confirmed") return BadRequest("Can only extend confirmed bookings.");
            if (dto.NewEndTime <= booking.EndTime) return BadRequest("New end time must be after current end time.");

            // Calculate New Total Price for the entire duration
            var newTotalPrice = await _pricingService.CalculatePriceAsync(booking.Listing.PricePerHour, booking.ListingId, booking.StartTime, dto.NewEndTime);
            var additionalCost = newTotalPrice - booking.TotalPrice;

            if (additionalCost < 0) return BadRequest("Error in price calculation.");

            if (additionalCost > 0)
            {
                var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == booking.DriverId);
                if (wallet == null || wallet.Balance < additionalCost)
                {
                    return BadRequest($"Insufficient funds. You need ₹{additionalCost} more.");
                }

                // Deduct
                wallet.Balance -= additionalCost;
                var transaction = new Transaction
                {
                    WalletId = wallet.Id,
                    Amount = additionalCost,
                    Type = "Debit",
                    Description = $"Extension for {booking.Listing.Title}"
                };
                _context.Wallets.Update(wallet);
                _context.Transactions.Add(transaction);
            }

            // Update Booking
            booking.EndTime = dto.NewEndTime;
            booking.TotalPrice = newTotalPrice;
            
            await _context.SaveChangesAsync();

            return Ok(new { message = "Booking extended successfully.", newEndTime = booking.EndTime, additionalCost });
        }
    }

    public class ExtendBookingDto
    {
        public DateTime NewEndTime { get; set; }
    }

    public class CreateBookingDto
    {
        public int ListingId { get; set; }
        public int DriverId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? PromoCode { get; set; }
    }
}
