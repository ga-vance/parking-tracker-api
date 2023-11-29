namespace ParkingTrackerAPI.Dtos.Authorization
{
    public class UpdateUserPasswordDto
    {

        public int UserId { get; set; }

        public required string OldPassword { get; set; }

        public required string NewPassword { get; set; }

    }
}