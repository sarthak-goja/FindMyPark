using System.ComponentModel.DataAnnotations;

namespace FindMyPark.API.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        public int ListingId { get; set; }

        public int DriverId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public decimal TotalPrice { get; set; }

        public string Status { get; set; } = "Confirmed"; // Confirmed, Cancelled, Completed

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public Listing Listing { get; set; }
        public User Driver { get; set; }
    }
}
