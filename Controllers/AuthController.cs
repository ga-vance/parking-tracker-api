using Microsoft.AspNetCore.Mvc;
using ParkingTrackerAPI.Dtos.Authorization;
using ParkingTrackerAPI.Dtos.User;
using ParkingTrackerAPI.Models;
using ParkingTrackerAPI.Services.AuthService;

namespace ParkingTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<string>>> Login(LoginUserDto request)
        {
            ServiceResponse<string> response = await _authService.Login(request);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // [HttpPatch]
    }
}