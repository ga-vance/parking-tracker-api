using Microsoft.EntityFrameworkCore;
using ParkingTrackerAPI.Models;

namespace ParkingTrackerAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Lot> Lots { get; set; }
        public DbSet<Visit> Visits { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<Vehicle>()
                .HasAlternateKey(v => new { v.PlateNumber, v.PlateRegion });

            base.OnModelCreating(modelBuilder);
        }
    }
}