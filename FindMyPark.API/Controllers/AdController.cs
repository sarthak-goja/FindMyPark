using FindMyPark.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace FindMyPark.API.Controllers
{
    public class Ad
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string LinkUrl { get; set; }
        public string Title { get; set; }
        public string TargetCity { get; set; } // "Delhi", "Mumbai", or "All"
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AdController : ControllerBase
    {
        // Mock Data Store
        private static readonly List<Ad> _ads = new()
        {
            new Ad { Id = 1, Title = "Car Wash @ ₹199", ImageUrl = "https://placehold.co/600x150/2980b9/ffffff?text=Premium+Car+Wash+@+199", LinkUrl = "/services", TargetCity = "All" },
            new Ad { Id = 2, Title = "Get FASTag", ImageUrl = "https://placehold.co/600x150/d35400/ffffff?text=Get+FASTag+-+Zero+Wait+Time", LinkUrl = "/fastag", TargetCity = "All" },
            new Ad { Id = 3, Title = "EV Charger Install", ImageUrl = "https://placehold.co/600x150/2ecc71/ffffff?text=Install+EV+Charger+at+Home", LinkUrl = "/ev", TargetCity = "Delhi" }
        };

        [HttpGet]
        public ActionResult<List<Ad>> GetAds([FromQuery] string city = "All")
        {
            var ads = _ads.Where(a => a.TargetCity == "All" || a.TargetCity.Equals(city, StringComparison.OrdinalIgnoreCase)).ToList();
            // Shuffle
            return Ok(ads.OrderBy(x => Guid.NewGuid()).Take(2));
        }
    }
}
