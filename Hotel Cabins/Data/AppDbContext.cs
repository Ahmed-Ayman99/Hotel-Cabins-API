using Hotel_Cabins.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Cabins.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Cabin> Cabins{ get; set; }
        public DbSet<Setting> Settings{ get; set; }
        public DbSet<Booking> Bookings{ get; set; }
        public AppDbContext(DbContextOptions options) :base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booking>()
                 .HasIndex(b => b.Status);

            // Unique constraint for cabin and guest combination
            modelBuilder.Entity<Booking>()
                .HasIndex(b => new { b.CabinId, b.GuestId })
                .IsUnique();
        }
    }
}
