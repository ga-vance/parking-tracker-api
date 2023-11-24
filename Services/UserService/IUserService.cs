using ParkingTrackerAPI.Dtos.User;
using ParkingTrackerAPI.Models;

namespace ParkingTrackerAPI.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<GetUserDto>> AddUser(AddUserDto newUser);

        Task<ServiceResponse<GetUserDto>> DeleteUser(int Id);

        Task<ServiceResponse<List<GetUserDto>>> GetAllUsers();

        Task<ServiceResponse<GetUserDto>> GetUserById(int Id);

    }
}