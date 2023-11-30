namespace ParkingTrackerAPI.Dtos.Visit
{
    public class AddVisitDto
    {
        public string? PlateNumber { get; set; }

        public string? PlateRegion { get; set; }

        public int LotId { get; set; }
    }
}