using ParkingTrackerAPI.Dtos.User;
using ParkingTrackerAPI.Models;

namespace ParkingTrackerAPI.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<GetUserDto>> AddUser(AddUserDto newUser);

    }
}