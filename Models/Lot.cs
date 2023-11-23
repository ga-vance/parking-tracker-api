using System.ComponentModel.DataAnnotations;

namespace ParkingTrackerAPI.Models
{
    public class Lot
    {
        [Key]
        public string LotCode { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string City { get; set; } = null!;

        // Navigation
        public ICollection<Visit> Visits { get; } = new List<Visit>();
    }
}