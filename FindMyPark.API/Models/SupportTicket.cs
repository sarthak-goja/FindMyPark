using System.ComponentModel.DataAnnotations;

namespace FindMyPark.API.Models
{
    public class SupportTicket
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } = "Open"; // Open, In-Progress, Resolved
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ResolvedAt { get; set; }
        public string? AdminResponse { get; set; }
    }
}
