using System.ComponentModel.DataAnnotations;

namespace FindMyPark.API.Models
{
    public class EventZone
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } // e.g., "Cricket Match at Stadium"
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double RadiusMeters { get; set; } // e.g., 2000m
        public decimal SurgeMultiplier { get; set; } // e.g., 2.0
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
