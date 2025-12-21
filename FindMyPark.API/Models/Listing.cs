using System.ComponentModel.DataAnnotations;

namespace FindMyPark.API.Models
{
    public class Listing
    {
        [Key]
        public int Id { get; set; }

        public int HostId { get; set; }
        
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string ZoneCode { get; set; } = string.Empty; // e.g. "1001"

        public int MaxDurationHours { get; set; } = 24;

        public bool IsOccupied { get; set; }
        public DateTime? LastSensorUpdate { get; set; }

        [Required]
        public string Address { get; set; } = string.Empty;

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public decimal PricePerHour { get; set; }

        public bool IsCovered { get; set; }

        public bool HasCCTV { get; set; }

        public bool HasEVCharger { get; set; }

        public bool IsApproved { get; set; } = false;

        // Navigation property
        public User Host { get; set; }
    }
}
