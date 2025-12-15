using FindMyPark.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindMyPark.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        private readonly AppDbContext _context;
        // In-memory store for demo purposes. In prod, use a DB table or Redis.
        private static Dictionary<string, bool> _featureFlags = new()
        {
            { "EnableAds", false },
            { "EnableEvents", true },
            { "EnableMaintenance", false },
            { "EnableAI", false }
        };

        private static Dictionary<string, string> _systemConfig = new()
        {
            { "MaintenanceMode", "false" },
            { "MaxBookingsPerUser", "5" },
            { "SupportEmail", "support@findmypark.com" }
        };

        public DeveloperController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("config")]
        public ActionResult<Dictionary<string, string>> GetConfig()
        {
            return Ok(_systemConfig);
        }

        [HttpPost("config")]
        public ActionResult UpdateConfig([FromBody] Dictionary<string, string> newConfig)
        {
            foreach (var kvp in newConfig)
            {
                if (_systemConfig.ContainsKey(kvp.Key))
                {
                    _systemConfig[kvp.Key] = kvp.Value;
                }
            }
            return Ok(_systemConfig);
        }

        [HttpGet("health")]
        public async Task<ActionResult<object>> GetSystemHealth()
        {
            var dbStatus = await _context.Database.CanConnectAsync() ? "Online" : "Offline";
            var activeUsers = await _context.Users.CountAsync();
            var activeBookings = await _context.Bookings.CountAsync(b => b.Status == "Confirmed");

            return Ok(new
            {
                Database = dbStatus,
                ActiveUsers = activeUsers,
                ActiveBookings = activeBookings,
                ServerTime = DateTime.UtcNow,
                Version = "1.0.0-Phase4"
            });
        }

        [HttpGet("flags")]
        public ActionResult<Dictionary<string, bool>> GetFeatureFlags()
        {
            return Ok(_featureFlags);
        }

        [HttpPost("flags/{key}")]
        public ActionResult ToggleFlag(string key, [FromQuery] bool enabled)
        {
            if (_featureFlags.ContainsKey(key))
            {
                _featureFlags[key] = enabled;
                return Ok(new { Key = key, Enabled = enabled });
            }
            return NotFound("Flag not found");
        }
    }
}
