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
            modelBuilder.Seed();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Vehicle>()
                .HasAlternateKey(v => new { v.PlateNumber, v.PlateRegion });

            modelBuilder.Entity<Lot>()
                .HasAlternateKey(l => l.LotCode);

            base.OnModelCreating(modelBuilder);
        }


    }
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lot>().HasData(
                new Lot
                {
                    LotId = 1,
                    LotCode = "STRCH",
                    LotName = "Strathcona Community Hospital",
                    City = "Sherwood Park"
                },
                new Lot
                {
                    LotId = 2,
                    LotCode = "LCH",
                    LotName = "Leduc Community Hospital",
                    City = "Leduc"
                },
                new Lot
                {
                    LotId = 3,
                    LotCode = "NECHC",
                    LotName = "Northeast Community Health Centre",
                    City = "Edmonton"
                },
                new Lot
                {
                    LotId = 4,
                    LotCode = "EEHC",
                    LotName = "East Edmonton Health Centre",
                    City = "Edmonton"
                },
                new Lot
                {
                    LotId = 5,
                    LotCode = "AHE",
                    LotName = "Alberta Hospital Edmonton",
                    City = "Edmonton"
                }
            );
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    UserName = "admin",
                    Email = "",
                    FirstName = "admin",
                    LastName = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin"),
                    IsAdmin = true
                }
            );
        }
    }
}