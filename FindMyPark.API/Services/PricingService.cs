using Microsoft.EntityFrameworkCore;

namespace FindMyPark.API.Services
{
    public interface IPricingService
    {
        Task<decimal> CalculatePriceAsync(decimal basePricePerHour, int listingId, DateTime start, DateTime end);
    }

    public class PricingService : IPricingService
    {
        private readonly FindMyPark.API.Data.AppDbContext _context;

        public PricingService(FindMyPark.API.Data.AppDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> CalculatePriceAsync(decimal basePricePerHour, int listingId, DateTime start, DateTime end)
        {
            decimal totalPrice = 0;
            var current = start;

            // Fetch listing location
            var listing = await _context.Listings.FindAsync(listingId);
            if (listing == null) return basePricePerHour * (decimal)(end - start).TotalHours;

            // Fetch active events
            var activeEvents = await _context.EventZones
                .Where(e => e.IsActive && e.EndTime > current && e.StartTime < end)
                .ToListAsync();

            decimal eventMultiplier = 1.0m;
            foreach (var evt in activeEvents)
            {
                if (IsWithinRadius(listing.Latitude, listing.Longitude, evt.Latitude, evt.Longitude, evt.RadiusMeters))
                {
                    // Take the highest multiplier if multiple events overlap
                    if (evt.SurgeMultiplier > eventMultiplier)
                    {
                        eventMultiplier = evt.SurgeMultiplier;
                    }
                }
            }

            while (current < end)
            {
                var nextHour = current.AddHours(1);
                if (nextHour > end) nextHour = end;

                var duration = (decimal)(nextHour - current).TotalHours;
                var hourMultiplier = 1.0m;

                // Time-based Peak Hours (9 AM - 6 PM Weekdays)
                if (current.DayOfWeek != DayOfWeek.Saturday && 
                    current.DayOfWeek != DayOfWeek.Sunday &&
                    current.Hour >= 9 && current.Hour < 18)
                {
                    hourMultiplier = 1.5m;
                }

                // Apply both multipliers (Event * Peak)
                // e.g., Event (2.0) * Peak (1.5) = 3.0x
                decimal finalMultiplier = hourMultiplier * eventMultiplier;

                totalPrice += (basePricePerHour * finalMultiplier) * duration;
                current = nextHour;
            }

            return Math.Round(totalPrice, 2);
        }

        private bool IsWithinRadius(double lat1, double lon1, double lat2, double lon2, double radiusMeters)
        {
            var R = 6371e3; // Earth radius in meters
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c;
            return d <= radiusMeters;
        }

        private double ToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }
    }
}
