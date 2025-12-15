using System.ComponentModel.DataAnnotations;

namespace FindMyPark.API.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public string LicensePlate { get; set; } // e.g., DL-01-AB-1234
        public string Type { get; set; } // 2W, 4W, EV
        public string Model { get; set; } // e.g., Swift Dzire

        public DateTime InsuranceExpiry { get; set; }
        public int PendingChallans { get; set; } = 0; // Mock count

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
