using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
        private readonly IConfiguration _configuration;

        public AuthService(IMapper mapper, ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<string>> Login(LoginUserDto request)
        {
            var serviceResponse = new ServiceResponse<string>();
            var user = await _context.Users.SingleAsync(u => u.UserName == request.UserName);
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

        public Task<ServiceResponse<GetUserDto>> UpdatePassword(UpdateUserPasswordDto request)
        {
            throw new NotImplementedException();
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!
            ));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}