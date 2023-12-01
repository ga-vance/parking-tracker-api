namespace ParkingTrackerAPI.Dtos.Visit
{
    public class AddVisitDto
    {
        public required string PlateNumber { get; set; }

        public required string PlateRegion { get; set; }

        public required int LotId { get; set; }
    }
}