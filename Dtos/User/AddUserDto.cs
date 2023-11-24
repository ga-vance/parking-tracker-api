namespace ParkingTrackerAPI.Dtos.User
{
    public class AddUserDto
    {
        public required string Email { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Password { get; set; }

        public bool IsAdmin { get; set; } = false;
    }
}