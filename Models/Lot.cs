
namespace ParkingTrackerAPI.Models
{
    public class Lot
    {
        public int LotId { get; set; }

        public string LotCode { get; set; } = null!;

        public string LotName { get; set; } = null!;

        public string City { get; set; } = null!;

        // Navigation
        public ICollection<Visit> Visits { get; } = new List<Visit>();
    }
}