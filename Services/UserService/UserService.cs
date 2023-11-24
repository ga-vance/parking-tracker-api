using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParkingTrackerAPI.Data;
using ParkingTrackerAPI.Dtos.User;
using ParkingTrackerAPI.Models;

namespace ParkingTrackerAPI.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserService(IMapper mapper, ApplicationDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetUserDto>> AddUser(AddUserDto newUser)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();
            var user = _mapper.Map<User>(newUser);
            // Generate Username to ensure uniqueness
            string username = user.FirstName[0] + "." + newUser.LastName;
            List<User> matchingUsers = await _context.Users.Where(u => u.UserName.Contains(username)).ToListAsync();
            if (matchingUsers.Count >= 1)
            {
                username += matchingUsers.Count;
            }
            // Add hashing
            user.UserName = username;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetUserDto>(user);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                if (ex.InnerException is null)
                {
                    serviceResponse.Message = ex.Message;
                }
                else serviceResponse.Message = ex.InnerException.Message;
            }

            return serviceResponse;
        }
    }
}