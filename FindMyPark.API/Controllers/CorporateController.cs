using FindMyPark.API.Data;
using FindMyPark.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindMyPark.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorporateController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CorporateController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Fleet>> RegisterFleet(RegisterFleetDto dto)
        {
            var user = await _context.Users.FindAsync(dto.AdminUserId);
            if (user == null) return NotFound("User not found");

            // Ideally check if user is already a fleet manager or upgrade them
            // user.Role = UserRole.FleetManager; 

            var fleet = new Fleet
            {
                CompanyName = dto.CompanyName,
                GSTNumber = dto.GSTNumber,
                AdminUserId = dto.AdminUserId
            };

            _context.Fleets.Add(fleet);
            await _context.SaveChangesAsync();
            
            // Assign user to this fleet (as admin)
            user.FleetId = fleet.Id;
            await _context.SaveChangesAsync();

            return Ok(fleet);
        }

        [HttpGet("dashboard/{fleetId}")]
        public async Task<ActionResult<object>> GetDashboard(int fleetId)
        {
            var fleet = await _context.Fleets.FindAsync(fleetId);
            if (fleet == null) return NotFound();

            var drivers = await _context.Users.Where(u => u.FleetId == fleetId).ToListAsync();
            var driverIds = drivers.Select(u => u.Id).ToList();

            var bookings = await _context.Bookings
                .Include(b => b.Listing)
                .Where(b => driverIds.Contains(b.DriverId))
                .OrderByDescending(b => b.StartTime)
                .ToListAsync();

            var totalSpent = bookings.Sum(b => b.TotalPrice);
            var activeBookings = bookings.Count(b => b.Status == "Confirmed" && b.EndTime > DateTime.UtcNow);

            return Ok(new
            {
                FleetName = fleet.CompanyName,
                TotalDrivers = drivers.Count,
                TotalSpent = totalSpent,
                ActiveBookings = activeBookings,
                RecentBookings = bookings.Take(5)
            });
        }
    }

    public class RegisterFleetDto
    {
        public string CompanyName { get; set; }
        public string GSTNumber { get; set; }
        public int AdminUserId { get; set; }
    }
}
