namespace ParkingTrackerAPI.Dtos.Lot
{
    public class GetLotDto
    {
        public int LotId { get; set; }

        public string LotCode { get; set; } = null!;

        public string LotName { get; set; } = null!;

        public string City { get; set; } = null!;
    }
}