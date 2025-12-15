using FindMyPark.API.Data;
using FindMyPark.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindMyPark.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PromotionsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<PromoCode>> CreatePromo([FromBody] PromoCode promo)
        {
            // Admin only check (omitted for MVP)
            _context.PromoCodes.Add(promo);
            await _context.SaveChangesAsync();
            return Ok(promo);
        }

        [HttpGet("validate/{code}")]
        public async Task<ActionResult<object>> ValidatePromo(string code)
        {
            var promo = await _context.PromoCodes
                .FirstOrDefaultAsync(p => p.Code == code.ToUpper() && p.IsActive);

            if (promo == null) return NotFound(new { Message = "Invalid Promo Code" });
            if (promo.ExpiryDate < DateTime.UtcNow) return BadRequest(new { Message = "Promo Code Expired" });
            if (promo.UsageCount >= promo.UsageLimit) return BadRequest(new { Message = "Usage Limit Reached" });

            return Ok(new 
            { 
                promo.Code, 
                promo.DiscountPercentage, 
                promo.MaxDiscountAmount 
            });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PromoCode>>> GetAllPromos()
        {
            return await _context.PromoCodes.ToListAsync();
        }
    }
}
