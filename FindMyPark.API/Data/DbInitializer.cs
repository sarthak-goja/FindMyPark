using FindMyPark.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FindMyPark.API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any users.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
                new User { Name = "John Host", Email = "host1@test.com", Role = UserRole.Host, PasswordHash = "hash" },
                new User { Name = "Jane Host", Email = "host2@test.com", Role = UserRole.Host, PasswordHash = "hash" },
                new User { Name = "Dave Driver", Email = "driver@test.com", Role = UserRole.Driver, PasswordHash = "hash" },
                new User { Name = "Super Admin", Email = "admin@test.com", Role = UserRole.Admin, PasswordHash = "hash" }
            };

            context.Users.AddRange(users);
            context.SaveChanges();

            // Seed Wallets
            var wallets = new Wallet[]
            {
                new Wallet { UserId = users[0].Id, Balance = 1000 }, // Host 1
                new Wallet { UserId = users[1].Id, Balance = 1000 }, // Host 2
                new Wallet { UserId = users[2].Id, Balance = 5000 }  // Driver Dave (Rich)
            };
            context.Wallets.AddRange(wallets);
            context.SaveChanges();

            // Listings (Linked to Hosts above)
            var listings = new Listing[]
            {
                new Listing
                {
                    Title = "Connaught Place Premium",
                    Address = "Block A, Connaught Place, New Delhi",
                    HostId = users[0].Id,
                    ZoneCode = "1001",
                    PricePerHour = 100,
                    Latitude = 28.6328, 
                    Longitude = 77.2197,
                    IsCovered = true,
                    HasCCTV = true,
                    HasEVCharger = true,
                    IsApproved = true,
                    IsOccupied = false,
                    LastSensorUpdate = DateTime.Now
                },
                new Listing
                {
                    Title = "DLF CyberHub Parking",
                    Address = "DLF Cyber City, Gurugram",
                    HostId = users[1].Id,
                    ZoneCode = "2002",
                    PricePerHour = 80,
                    Latitude = 28.4950,
                    Longitude = 77.0895,
                    IsCovered = true,
                    HasCCTV = true,
                    HasEVCharger = false,
                    IsApproved = true,
                    IsOccupied = false,
                    LastSensorUpdate = DateTime.Now
                },
                new Listing
                {
                    Title = "Select Citywalk Basement",
                    Address = "Saket District Centre, New Delhi",
                    HostId = users[0].Id,
                    ZoneCode = "3003",
                    PricePerHour = 60,
                    Latitude = 28.5285,
                    Longitude = 77.2193,
                    IsCovered = true,
                    HasCCTV = true,
                    HasEVCharger = true,
                    IsApproved = true,
                    IsOccupied = true, // Busy spot
                    LastSensorUpdate = DateTime.Now
                }
            };

            context.Listings.AddRange(listings);
            context.SaveChanges();
        }
    }
}
