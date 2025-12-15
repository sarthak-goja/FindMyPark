using FindMyPark.API.Data;
using FindMyPark.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindMyPark.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KycController : ControllerBase
    {
        private readonly AppDbContext _context;

        public KycController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadDocument([FromForm] KycUploadDto dto)
        {
            // In a real app, we would process the IFormFile and save to cloud storage.
            // Here we just mock the file path.
            
            var doc = new KycDocument
            {
                UserId = dto.UserId,
                DocumentType = dto.DocumentType,
                FilePath = $"/uploads/{dto.DocumentType}_{Guid.NewGuid()}.jpg"
            };

            _context.KycDocuments.Add(doc);
            
            // Auto-update status to Pending if not already
            var user = await _context.Users.FindAsync(dto.UserId);
            if (user != null && user.KycStatus == "None")
            {
                user.KycStatus = "Pending";
            }

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Document uploaded successfully", Path = doc.FilePath });
        }

        [HttpGet("status/{userId}")]
        public async Task<ActionResult<string>> GetStatus(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return NotFound();
            return Ok(new { Status = user.KycStatus });
        }

        [HttpPost("verify/{userId}")]
        public async Task<IActionResult> VerifyUser(int userId, [FromBody] string status) // status: Verified or Rejected
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return NotFound();

            user.KycStatus = status;
            await _context.SaveChangesAsync();
            return Ok(new { Message = $"User KYC status updated to {status}" });
        }

        [HttpGet("pending")]
        public async Task<ActionResult<IEnumerable<object>>> GetPendingKycUsers()
        {
            var users = await _context.Users
                .Where(u => u.KycStatus == "Pending")
                .Select(u => new 
                { 
                    u.Id, 
                    u.Name, 
                    u.Email,
                    Documents = _context.KycDocuments.Where(d => d.UserId == u.Id).ToList() 
                })
                .ToListAsync();
                
            return Ok(users);
        }
    }

    public class KycUploadDto
    {
        public int UserId { get; set; }
        public string DocumentType { get; set; }
        // public IFormFile File { get; set; } // Mocking file upload for simplicity
    }
}
