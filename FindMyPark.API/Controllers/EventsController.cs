using FindMyPark.API.Data;
using FindMyPark.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindMyPark.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EventsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<EventZone>> CreateEvent(EventZone eventZone)
        {
            _context.EventZones.Add(eventZone);
            await _context.SaveChangesAsync();
            return Ok(eventZone);
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<EventZone>>> GetActiveEvents()
        {
            return await _context.EventZones
                .Where(e => e.IsActive && e.EndTime > DateTime.UtcNow)
                .ToListAsync();
        }

        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateEvent(int id)
        {
            var evt = await _context.EventZones.FindAsync(id);
            if (evt == null) return NotFound();

            evt.IsActive = false;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
