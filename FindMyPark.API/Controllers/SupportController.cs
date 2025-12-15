using FindMyPark.API.Data;
using FindMyPark.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindMyPark.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SupportController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<SupportTicket>> CreateTicket(CreateTicketDto dto)
        {
            var ticket = new SupportTicket
            {
                UserId = dto.UserId,
                Subject = dto.Subject,
                Description = dto.Description
            };
            
            _context.SupportTickets.Add(ticket);
            await _context.SaveChangesAsync();
            return Ok(ticket);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<SupportTicket>>> GetUserTickets(int userId)
        {
            return await _context.SupportTickets
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<SupportTicket>>> GetAllTickets()
        {
            // Admin only check omitted for MVP
            return await _context.SupportTickets
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        [HttpPut("{id}/resolve")]
        public async Task<ActionResult> ResolveTicket(int id, [FromBody] ResolveTicketDto dto)
        {
            var ticket = await _context.SupportTickets.FindAsync(id);
            if (ticket == null) return NotFound();

            ticket.Status = "Resolved";
            ticket.ResolvedAt = DateTime.UtcNow;
            ticket.AdminResponse = dto.Response;

            await _context.SaveChangesAsync();
            return Ok(ticket);
        }

        [HttpGet("faqs")]
        public ActionResult<List<FaqItem>> GetFaqs()
        {
            var faqs = new List<FaqItem>
            {
                new FaqItem { Id = 1, Category = "Booking", Question = "How to cancel a booking?", Answer = "You can cancel a booking from the 'My Bookings' tab updates at least 1 hour before start time." },
                new FaqItem { Id = 2, Category = "Payments", Question = "Is my wallet money refundable?", Answer = "Wallet balance is non-refundable but never expires." },
                new FaqItem { Id = 3, Category = "General", Question = "How to contact support?", Answer = "Use the 'Create Ticket' button on this page." },
                new FaqItem { Id = 4, Category = "Security", Question = "Is my car safe?", Answer = "We verify all hosts via KYC, but parking is at owner's risk." }
            };
            return Ok(faqs);
        }
    }

    public class CreateTicketDto
    {
        public int UserId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
    }

    public class ResolveTicketDto
    {
        public string Response { get; set; }
    }
}
