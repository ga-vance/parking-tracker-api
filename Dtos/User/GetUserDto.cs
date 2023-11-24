namespace ParkingTrackerAPI.Dtos.User
{
    public class GetUserDto
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = null!;

        public bool IsAdmin { get; set; } = false;
    }
}