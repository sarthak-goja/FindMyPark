using FindMyPark.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindMyPark.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("host/{hostId}")]
        public async Task<ActionResult<HostDashboardStats>> GetHostStats(int hostId)
        {
            var listings = await _context.Listings.Where(l => l.HostId == hostId).ToListAsync();
            var listingIds = listings.Select(l => l.Id).ToList();

            var bookings = await _context.Bookings
                .Where(b => listingIds.Contains(b.ListingId))
                .ToListAsync();

            var stats = new HostDashboardStats
            {
                TotalListings = listings.Count,
                TotalBookings = bookings.Count,
                TotalEarnings = bookings.Where(b => b.Status == "Confirmed").Sum(b => b.TotalPrice),
                ActiveBookings = bookings.Count(b => b.StartTime <= DateTime.UtcNow && b.EndTime >= DateTime.UtcNow && b.Status == "Confirmed"),
                RecentBookings = bookings.OrderByDescending(b => b.CreatedAt).Take(5).ToList()
            };

            return stats;
        }
    }

    public class HostDashboardStats
    {
        public int TotalListings { get; set; }
        public int TotalBookings { get; set; }
        public decimal TotalEarnings { get; set; }
        public int ActiveBookings { get; set; }
        public object RecentBookings { get; set; }
    }

    public class HeatmapPoint
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
        public double Intensity { get; set; } // 0.0 to 1.0 (Demand)
    }

    // Extend Controller
    public partial class DashboardController
    {
        [HttpGet("analytics/heatmap")]
        public IActionResult GetHeatmapData()
        {
            // Mock Data representing high demand areas in Delhi NCR
            var points = new List<HeatmapPoint>
            {
                new HeatmapPoint { Lat = 28.6139, Lng = 77.2090, Intensity = 0.9 }, // CP
                new HeatmapPoint { Lat = 28.5355, Lng = 77.3910, Intensity = 0.8 }, // Noida Sec 18
                new HeatmapPoint { Lat = 28.4595, Lng = 77.0266, Intensity = 0.95 }, // Cyber Hub
                new HeatmapPoint { Lat = 28.7041, Lng = 77.1025, Intensity = 0.6 }, // Rohini
                new HeatmapPoint { Lat = 28.5603, Lng = 77.2573, Intensity = 0.7 }  // Lajpat Nagar
            };
            return Ok(points);
        }
    }
}
