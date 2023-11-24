namespace ParkingTrackerAPI.Dtos.User
{
    public class UpdateUserDto
    {
        public int UserId { get; set; }

        public string Email { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public bool IsAdmin { get; set; } = false;

    }
}