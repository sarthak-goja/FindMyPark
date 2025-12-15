using FindMyPark.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindMyPark.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IoTController : ControllerBase
    {
        private readonly AppDbContext _context;

        public IoTController(AppDbContext context)
        {
            _context = context;
        }

        // Endpoint for Hardware Sensors to hit
        [HttpPost("sensor/{listingId}")]
        public async Task<IActionResult> UpdateSensorData(int listingId, [FromBody] SensorDataDto dto)
        {
            var listing = await _context.Listings.FindAsync(listingId);
            if (listing == null) return NotFound("Sensor not linked to valid listing");

            listing.IsOccupied = dto.IsOccupied;
            listing.LastSensorUpdate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Sensor Data Synced", CurrentStatus = listing.IsOccupied ? "Occupied" : "Free" });
        }

        // Simulating a parking turnover (for demo purposes)
        [HttpPost("simulate/{listingId}")]
        public async Task<IActionResult> SimulateToggle(int listingId)
        {
            var listing = await _context.Listings.FindAsync(listingId);
            if (listing == null) return NotFound();

            listing.IsOccupied = !listing.IsOccupied;
            listing.LastSensorUpdate = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Simulated Toggle", IsOccupied = listing.IsOccupied });
        }
    }

    public class SensorDataDto
    {
        public bool IsOccupied { get; set; }
        public double BatteryLevel { get; set; }
        public string DeviceId { get; set; }
    }
}
