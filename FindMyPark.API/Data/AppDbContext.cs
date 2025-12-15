using Microsoft.EntityFrameworkCore;
using FindMyPark.API.Models;

namespace FindMyPark.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<KycDocument> KycDocuments { get; set; }
        public DbSet<PromoCode> PromoCodes { get; set; }
        public DbSet<Fleet> Fleets { get; set; }
        public DbSet<SupportTicket> SupportTickets { get; set; }
        public DbSet<EventZone> EventZones { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Enum conversion
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>();
                
            modelBuilder.Entity<Listing>()
                .Property(l => l.PricePerHour)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Booking>()
                .Property(b => b.TotalPrice)
                .HasColumnType("decimal(18,2)");
        }
    }
}
