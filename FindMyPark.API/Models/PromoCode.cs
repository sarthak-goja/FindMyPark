using System.ComponentModel.DataAnnotations;

namespace FindMyPark.API.Models
{
    public class PromoCode
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; } // e.g., "SAVE10"
        public decimal DiscountPercentage { get; set; } // e.g., 10.0
        public decimal MaxDiscountAmount { get; set; } // Cap, e.g., 50
        public DateTime ExpiryDate { get; set; }
        public int UsageLimit { get; set; } // Total times it can be used
        public int UsageCount { get; set; } = 0;
        public bool IsActive { get; set; } = true;
    }
}
