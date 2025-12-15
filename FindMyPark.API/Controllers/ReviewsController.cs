using FindMyPark.API.Data;
using FindMyPark.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindMyPark.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReviewsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("listing/{listingId}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews(int listingId)
        {
            var reviews = await _context.Reviews
                .Where(r => r.ListingId == listingId)
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new ReviewDto
                {
                    DriverName = r.Driver.Name,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedAt = r.CreatedAt
                })
                .ToListAsync();

            return Ok(reviews);
        }

        [HttpPost]
        public async Task<ActionResult<Review>> AddReview(CreateReviewDto dto)
        {
            var booking = await _context.Bookings.FindAsync(dto.BookingId);
            if (booking == null) return NotFound("Booking not found");
            if (booking.DriverId != dto.DriverId) return Forbid(); // Ensure only the booker can review

            var review = new Review
            {
                BookingId = dto.BookingId,
                ListingId = booking.ListingId,
                DriverId = dto.DriverId,
                Rating = dto.Rating,
                Comment = dto.Comment
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReviews), new { listingId = review.ListingId }, review);
        }
    }

    public class ReviewDto
    {
        public string DriverName { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateReviewDto
    {
        public int BookingId { get; set; }
        public int DriverId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
