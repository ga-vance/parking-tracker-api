namespace ParkingTrackerAPI.Models
{
    public class Visit
    {
        public int VisitId { get; set; }

        public int VehicleId { get; set; }

        public string LotCode { get; set; } = null!;

        public DateTime DateTime { get; set; }

        public int UserId { get; set; }


        // Navigation Links
        public Vehicle Vehicle { get; set; } = null!;
        public Lot Lot { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}