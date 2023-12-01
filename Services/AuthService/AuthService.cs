using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ParkingTrackerAPI.Data;
using ParkingTrackerAPI.Dtos.Authorization;
using ParkingTrackerAPI.Dtos.User;
using ParkingTrackerAPI.Models;

namespace ParkingTrackerAPI.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IMapper mapper, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<string>> Login(LoginUserDto request)
        {
            var serviceResponse = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName);
            if (user is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Not Found";
                return serviceResponse;
            }
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Not Found";
                return serviceResponse;
            }
            string token = CreateToken(user);
            serviceResponse.Data = token;
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> UpdatePassword(UpdateUserPasswordDto request)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();
            var claimId = _httpContextAccessor?.HttpContext?.User.FindFirstValue("Id");
            if (claimId is not null)
            {
                if (claimId != request.UserId.ToString())
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Forbidden Request";
                    return serviceResponse;
                }
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == request.UserId);
                if (!BCrypt.Net.BCrypt.Verify(request.OldPassword, user!.PasswordHash))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Wrong Password";
                    return serviceResponse;
                }
                try
                {
                    user!.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetUserDto>(user);
                    return serviceResponse;
                }
                catch (Exception e)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = e.Message;
                    return serviceResponse;
                }
            }

            return serviceResponse;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("Id", user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.IsAdmin? "Admin" : "User")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                Environment.GetEnvironmentVariable("SIGNING_TOKEN")!
            ));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}