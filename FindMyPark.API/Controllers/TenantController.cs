using FindMyPark.API.Data;
using FindMyPark.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindMyPark.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TenantController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("config")]
        public async Task<ActionResult<Tenant>> GetTenantConfig([FromQuery] string domain)
        {
            // In a real app, we might extract domain from the Host header
            var tenant = await _context.Tenants.FirstOrDefaultAsync(t => t.Domain == domain && t.IsActive);
            
            if (tenant == null)
            {
                // Return default branding if not found
                return Ok(new Tenant
                {
                    Name = "FindMyPark",
                    PrimaryColor = "#e94560", // Default Pink/Red
                    LogoUrl = "assets/logo.png"
                });
            }

            return Ok(tenant);
        }

        [HttpPost]
        public async Task<ActionResult<Tenant>> CreateTenant(Tenant tenant)
        {
            _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync();
            return Ok(tenant);
        }
    }
}
