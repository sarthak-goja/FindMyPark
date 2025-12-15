using System.ComponentModel.DataAnnotations;

namespace FindMyPark.API.Models
{
    public class FaqItem
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Category { get; set; } // "Payments", "Booking", "General"
    }
}
