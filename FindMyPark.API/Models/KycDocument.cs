using System.ComponentModel.DataAnnotations;

namespace FindMyPark.API.Models
{
    public class KycDocument
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string DocumentType { get; set; } // Aadhaar, PAN, Selfie
        public string FilePath { get; set; } // Mock URL or path
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
