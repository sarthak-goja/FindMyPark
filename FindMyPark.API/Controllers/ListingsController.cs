using FindMyPark.API.Data;
using FindMyPark.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindMyPark.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ListingsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Listing>>> GetListings()
        {
            return await _context.Listings.Include(l => l.Host).ToListAsync();
        }
        
        [HttpGet("host/{hostId}")]
        public async Task<ActionResult<IEnumerable<Listing>>> GetMyListings(int hostId)
        {
            return await _context.Listings.Where(l => l.HostId == hostId).ToListAsync();
        }

        [HttpGet("zone/{zoneCode}")]
        public async Task<ActionResult<Listing>> GetListingByZone(string zoneCode)
        {
            var listing = await _context.Listings.Include(l => l.Host)
                                                 .FirstOrDefaultAsync(l => l.ZoneCode == zoneCode);

            if (listing == null)
            {
                return NotFound(new { message = "Invalid Zone Code" });
            }

            return listing;
        }

        [HttpPost]
        public async Task<ActionResult<Listing>> PostListing(CreateListingDto dto)
        {
            var listing = new Listing
            {
                HostId = dto.HostId,
                Title = dto.Title,
                Address = dto.Address,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                PricePerHour = dto.PricePerHour,
                IsCovered = dto.IsCovered,
                HasCCTV = dto.HasCCTV,
                HasEVCharger = dto.HasEVCharger
            };

            _context.Listings.Add(listing);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetListings), new { id = listing.Id }, listing);
        }

        [HttpGet("pending")]
        public async Task<ActionResult<IEnumerable<Listing>>> GetPendingListings()
        {
            return await _context.Listings
                .Include(l => l.Host)
                .Where(l => !l.IsApproved)
                .ToListAsync();
        }

        [HttpPost("{id}/approve")]
        public async Task<IActionResult> ApproveListing(int id)
        {
            var listing = await _context.Listings.FindAsync(id);
            if (listing == null) return NotFound();

            listing.IsApproved = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

    public class CreateListingDto
    {
        public int HostId { get; set; } // Temporarily passed manually, later via Token
        public string Title { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public decimal PricePerHour { get; set; }
        public bool IsCovered { get; set; }
        public bool HasCCTV { get; set; }
        public bool HasEVCharger { get; set; }
    }
}
