using System.ComponentModel.DataAnnotations;

namespace FindMyPark.API.Models
{
    public class Fleet
    {
        [Key]
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string GSTNumber { get; set; }
        public int AdminUserId { get; set; } // The user who manages this fleet
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
