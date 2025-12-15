using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindMyPark.API.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        public int WalletId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public string Type { get; set; } // "Credit", "Debit"

        public string Description { get; set; } // e.g., "Added Funds", "Booking #123"

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Wallet Wallet { get; set; }
    }
}
