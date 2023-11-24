using Microsoft.AspNetCore.Mvc;
using ParkingTrackerAPI.Dtos.User;
using ParkingTrackerAPI.Models;
using ParkingTrackerAPI.Services.UserService;

namespace ParkingTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> AddUser(AddUserDto newUser)
        {
            ServiceResponse<GetUserDto> response = await _userService.AddUser(newUser);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
    }
}