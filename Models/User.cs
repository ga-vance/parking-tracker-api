namespace ParkingTrackerAPI.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public bool IsAdmin { get; set; } = false;

        // Navigation
        public ICollection<Visit> Visits { get; } = new List<Visit>();
    }
}