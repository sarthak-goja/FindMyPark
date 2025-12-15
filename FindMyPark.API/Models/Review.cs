using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindMyPark.API.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        public int BookingId { get; set; } // Links to the specific transaction
        public int ListingId { get; set; }
        public int DriverId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Booking Booking { get; set; }
        public Listing Listing { get; set; }
        public User Driver { get; set; }
    }
}
