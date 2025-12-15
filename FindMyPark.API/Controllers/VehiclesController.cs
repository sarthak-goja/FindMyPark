using FindMyPark.API.Data;
using FindMyPark.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindMyPark.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VehiclesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetMyVehicles(int userId)
        {
            return await _context.Vehicles.Where(v => v.UserId == userId).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Vehicle>> AddVehicle(CreateVehicleDto dto)
        {
            // Mock Challan/Insurance Logic randomly
            var random = new Random();
            var mockChallans = random.Next(0, 3); // 0 to 2 challans
            var mockInsuranceExpiry = DateTime.UtcNow.AddMonths(random.Next(1, 12));

            var vehicle = new Vehicle
            {
                UserId = dto.UserId,
                LicensePlate = dto.LicensePlate.ToUpper(),
                Type = dto.Type,
                Model = dto.Model,
                InsuranceExpiry = mockInsuranceExpiry,
                PendingChallans = mockChallans
            };

            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMyVehicles), new { userId = vehicle.UserId }, vehicle);
        }

        [HttpPost("check-challan/{plate}")]
        public IActionResult CheckChallan(string plate)
        {
            // External RTO API would go here.
            // Mocking response
            return Ok(new { Plate = plate, Status = "Checked", PendingAmount = new Random().Next(0, 2000) });
        }
    }

    public class CreateVehicleDto
    {
        public int UserId { get; set; }
        public string LicensePlate { get; set; }
        public string Type { get; set; }
        public string Model { get; set; }
    }
}
