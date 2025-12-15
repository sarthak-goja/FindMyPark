using System.ComponentModel.DataAnnotations;

namespace FindMyPark.API.Models
{
    public class Tenant
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } // e.g. "DLF CyberHub"
        public string Domain { get; set; } // e.g. "dlf.findmypark.com" or just "dlf" for simulation
        public string PrimaryColor { get; set; } // e.g. "#FF5733"
        public string LogoUrl { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
