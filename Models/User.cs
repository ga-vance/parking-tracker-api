using System.ComponentModel.DataAnnotations;

namespace ParkingTrackerAPI.Models
{
    public class User
    {
        public int UserId { get; set; }

        [StringLength(255)]
        public string UserName { get; set; } = null!;

        [StringLength(255)]
        public string Email { get; set; } = null!;

        [StringLength(255)]
        public string FirstName { get; set; } = null!;

        [StringLength(255)]
        public string LastName { get; set; } = null!;

        [StringLength(255)]
        public string Password { get; set; } = null!;

        public bool IsAdmin { get; set; }
    }
}