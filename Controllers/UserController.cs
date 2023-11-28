using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingTrackerAPI.Dtos.User;
using ParkingTrackerAPI.Models;
using ParkingTrackerAPI.Services.UserService;

namespace ParkingTrackerAPI.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> GetAllUsers()
        {
            ServiceResponse<List<GetUserDto>> response = await _userService.GetAllUsers();
            return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetUserById(int Id)
        {
            ServiceResponse<GetUserDto> response = await _userService.GetUserById(Id);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> AddUser(AddUserDto newUser)
        {
            ServiceResponse<GetUserDto> response = await _userService.AddUser(newUser);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPatch]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> UpdateUser(UpdateUserDto updatedUser)
        {
            ServiceResponse<GetUserDto> response = await _userService.UpdateUser(updatedUser);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> DeleteUser(int Id)
        {
            ServiceResponse<GetUserDto> response = await _userService.DeleteUser(Id);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}