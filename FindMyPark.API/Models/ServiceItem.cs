using System.ComponentModel.DataAnnotations;

namespace FindMyPark.API.Models
{
    public class ServiceItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Icon { get; set; } // e.g., "🚿" or "🚗"
        public string Description { get; set; }
    }
}
