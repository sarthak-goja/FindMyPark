using FindMyPark.API.Data;
using Microsoft.EntityFrameworkCore;

namespace FindMyPark.API.Services
{
    public interface IAIService
    {
        Task<List<OccupancyForecast>> PredictOccupancyAsync(int listingId, DateTime date);
    }

    public class OccupancyForecast
    {
        public int Hour { get; set; }
        public int OccupancyPercentage { get; set; } // 0-100
        public string Level { get; set; } // Low, Medium, High
    }

    public class AIService : IAIService
    {
        private readonly AppDbContext _context;

        public AIService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<OccupancyForecast>> PredictOccupancyAsync(int listingId, DateTime date)
        {
            // In a real scenario, this would use ML.NET or call a Python service.
            // Here we simulate prediction based on:
            // 1. Day of week (Weekends are busier for malls, Weekdays for offices)
            // 2. Random variation to simulate "Intelligence"
            
            var forecasts = new List<OccupancyForecast>();
            var random = new Random(listingId + date.DayOfYear); // Consistent seed for same request

            bool isWeekend = date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;

            for (int i = 0; i < 24; i++)
            {
                int baseLoad = 0;

                if (isWeekend)
                {
                    // Weekend Pattern: Late start, peak afternoon/evening
                    if (i >= 11 && i <= 21) baseLoad = 80;
                    else if (i >= 9) baseLoad = 40;
                }
                else
                {
                    // Weekday Pattern: Office hours (9-6)
                    if (i >= 9 && i <= 18) baseLoad = 90;
                    else if (i >= 7) baseLoad = 50;
                }

                // Add variation
                int noise = random.Next(-15, 15);
                int finalLoad = Math.Clamp(baseLoad + noise, 5, 100);

                forecasts.Add(new OccupancyForecast
                {
                    Hour = i,
                    OccupancyPercentage = finalLoad,
                    Level = finalLoad > 80 ? "High" : finalLoad > 50 ? "Medium" : "Low"
                });
            }

            return await Task.FromResult(forecasts);
        }
    }
}
