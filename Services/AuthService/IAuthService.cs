using ParkingTrackerAPI.Dtos.Authorization;
using ParkingTrackerAPI.Dtos.User;
using ParkingTrackerAPI.Models;

namespace ParkingTrackerAPI.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> Login(LoginUserDto request);

        Task<ServiceResponse<GetUserDto>> UpdatePassword(UpdateUserPasswordDto request);
    }
}