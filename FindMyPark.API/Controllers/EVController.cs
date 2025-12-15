using Microsoft.AspNetCore.Mvc;

namespace FindMyPark.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EVController : ControllerBase
    {
        [HttpGet("chargers/{listingId}")]
        public IActionResult GetChargers(int listingId)
        {
            // MOCK OCPI Implementation
            // Simulating fetching data from an external Charge Point Operator (CPO)
            
            var random = new Random();
            var chargers = new List<object>();
            
            // Generate 1-3 chargers per listing
            int count = random.Next(1, 4);

            for (int i = 0; i < count; i++)
            {
                bool isFast = random.Next(0, 2) == 1;
                bool isAvailable = random.Next(0, 10) > 2; // 70% chance available

                chargers.Add(new 
                { 
                    Id = $"EV-{listingId}-{i+1}",
                    Type = isFast ? "CCS2 (DC Fast)" : "Type 2 (AC)",
                    Power = isFast ? $"{random.Next(50, 151)}kW" : "7.2kW",
                    Status = isAvailable ? "AVAILABLE" : "CHARGING",
                    PricePerKwh = isFast ? 18.00 : 10.00
                });
            }
            
            return Ok(chargers);
        }
    }
}
