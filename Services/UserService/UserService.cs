using System.Text.RegularExpressions;
using System.Globalization;
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
            // Validate password length
            if (newUser.Password.Length < 8)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Password must be 8 characters or longer";
                return serviceResponse;
            }
            if (!IsValidEmail(user.Email))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Invalid email address";
                return serviceResponse;
            }
            // Validate unique email
            var checkUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == newUser.Email.ToLower());
            if (checkUser is not null)
            {
                serviceResponse.Message = "Please enter a unique email address";
                serviceResponse.Success = false;
                return serviceResponse;
            }
            // Generate Username to ensure uniqueness
            string username = (user.FirstName[0] + "." + newUser.LastName).ToLower();
            List<User> matchingUsers = await _context.Users.Where(u => u.UserName.Contains(username)).ToListAsync();
            if (matchingUsers.Count >= 1)
            {
                username += matchingUsers.Count;
            }
            user.UserName = username;
            user.FirstName = user.FirstName.ToLower();
            user.LastName = user.LastName.ToLower();
            user.Email = user.Email.ToLower();

            // Add password hashing
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

        public async Task<ServiceResponse<GetUserDto>> UpdateUser(UpdateUserDto updatedUser)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();
            var user = await _context.Users.FindAsync(updatedUser.UserId);
            if (user is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Not Found";
                return serviceResponse;
            }

            if (!IsValidEmail(updatedUser.Email))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Invalid email address";
                return serviceResponse;
            }
            // Validate unique email
            var checkUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == updatedUser.Email.ToLower());
            if (checkUser is not null)
            {
                serviceResponse.Message = "Please enter a unique email address";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            user.FirstName = updatedUser.FirstName.ToLower();
            user.LastName = updatedUser.LastName.ToLower();
            user.Email = updatedUser.Email.ToLower();
            user.IsAdmin = updatedUser.IsAdmin;
            await _context.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<GetUserDto>(user);
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

        public async Task<ServiceResponse<List<GetUserDto>>> GetAllUsers()
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();
            var users = await _context.Users.ToListAsync();
            serviceResponse.Data = users.Select(u => _mapper.Map<GetUserDto>(u)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> GetUserById(int Id)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();
            var user = await _context.Users.FindAsync(Id);
            if (user is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Not Found";
                return serviceResponse;
            }
            serviceResponse.Data = _mapper.Map<GetUserDto>(user);
            return serviceResponse;

        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                Console.WriteLine(e);
                return false;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}