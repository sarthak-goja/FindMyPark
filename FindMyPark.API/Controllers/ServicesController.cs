using FindMyPark.API.Data;
using FindMyPark.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace FindMyPark.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ServicesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ServiceItem>> GetServices()
        {
            // Return mock data for now to avoid DB migration complexity in this phase
            var services = new List<ServiceItem>
            {
                new ServiceItem { Id = 1, Name = "Premium Car Wash", Price = 199, Icon = "🚿", Description = "Exterior foam wash + Interior vacuum" },
                new ServiceItem { Id = 2, Name = "Valet Parking", Price = 99, Icon = "🤵", Description = "Drop off at entrance" },
                new ServiceItem { Id = 3, Name = "EV Fast Charge", Price = 250, Icon = "⚡", Description = "Priority charging slot" }
            };
            return Ok(services);
        }
    }
}
