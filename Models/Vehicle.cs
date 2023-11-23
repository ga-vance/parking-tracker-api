using System.ComponentModel.DataAnnotations;

namespace ParkingTrackerAPI.Models
{
    public class Vehicle
    {
        public int VehicleId { get; set; }

        [StringLength(12)]
        public string PlateNumber { get; set; } = null!;

        [StringLength(2)]
        public string PlateRegion { get; set; } = null!;

        // Navigation
        public ICollection<Visit> Visits { get; } = new List<Visit>();

    }
}