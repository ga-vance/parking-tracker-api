namespace ParkingTrackerAPI.Dtos.Lot
{
    public class AddLotDto
    {
        public string LotCode { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string City { get; set; } = null!;
    }
}