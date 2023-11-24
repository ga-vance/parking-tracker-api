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
            string username = user.FirstName[0] + "." + newUser.LastName;
            // Validate password length
            if (newUser.Password.Length < 8)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Password must be 8 characters or longer";
                return serviceResponse;
            }
            // Validate unique email
            var checkUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == newUser.Email);
            if (checkUser is not null)
            {
                serviceResponse.Message = "Please enter a unique email address";
                serviceResponse.Success = false;
                return serviceResponse;
            }
            // Generate Username to ensure uniqueness
            List<User> matchingUsers = await _context.Users.Where(u => u.UserName.Contains(username)).ToListAsync();
            if (matchingUsers.Count >= 1)
            {
                username += matchingUsers.Count;
            }
            // Add password hashing
            user.UserName = username;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
            // Save new user
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

        public async Task<ServiceResponse<GetUserDto>> DeleteUser(int Id)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == Id);
            if (user is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "No user found";
                return serviceResponse;
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<GetUserDto>(user);
            return serviceResponse;
        }

        // public async Task<ServiceResponse<List<GetUserDto>>> GetAllUsers()
        // {
        //     throw new NotImplementedException();
        // }

        // public async Task<ServiceResponse<GetUserDto>> GetUserById(int Id)
        // {
        //     throw new NotImplementedException();
        // }
    }
}