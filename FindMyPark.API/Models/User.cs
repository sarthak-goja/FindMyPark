using System.ComponentModel.DataAnnotations;

namespace FindMyPark.API.Models
{
    public enum UserRole
    {
        Driver,
        Host,
        Admin,
        SupportAgent,
        Developer,
        FleetManager
    }

    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public UserRole Role { get; set; }

        public bool IsKycVerified { get; set; } = false;

        public int Points { get; set; } = 0; // Gamification Points
        public string KycStatus { get; set; } = "Pending"; // Pending, Verified, Rejected
        public int? FleetId { get; set; } // Nullable, if belongs to a fleet
        
        // Navigation properties will be added later
    }
}
