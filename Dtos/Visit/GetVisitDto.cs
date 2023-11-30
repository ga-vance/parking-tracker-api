namespace ParkingTrackerAPI.Dtos.Visit
{
    public class GetVisitDto
    {
        public int VisitId { get; set; }

        public string PlateNumber { get; set; } = null!;

        public string PlateRegion { get; set; } = null!;

        public int LotId { get; set; }

        public string LotCode { get; set; } = null!;

        public string LotName { get; set; } = null!;

        public DateTime DateTime { get; set; }

        public int UserId { get; set; }

    }
}