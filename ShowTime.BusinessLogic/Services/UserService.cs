using System.Threading.Tasks;
using ShowTime.BusinessLogic.Abstractions;
using ShowTime.BusinessLogic.Dtos;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess;
using Microsoft.EntityFrameworkCore;
using ShowTime.DataAccess.GenericInterface;

namespace ShowTime.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepo;
        public UserService(IGenericRepository<User> userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginDto login)
        {
            try
            {
                var users = await _userRepo.GetAllAsync();
                var matchedUser = users.FirstOrDefault(u => u.Email == login.Email && u.Password == login.Password);
                
                if (matchedUser == null)
                {
                    return null;
                }

                return new LoginResponseDto
                {
                    Role = matchedUser.Role
                };
            }
            catch(Exception ex)
            {
                // Log the exception (not implemented here)
                return null;
            }
        }
    
        public async Task<UserCreateDto> CreateUserAsync(UserCreateDto userCreateDto)
        {
            try
            {
                // Check if user already exists
                var users = await _userRepo.GetAllAsync();
                var existingUser = users.FirstOrDefault(u => u.Email == userCreateDto.Email);
                
                if (existingUser != null)
                {
                    throw new InvalidOperationException("A user with this email already exists.");
                }

                var user = new User
                {
                    Email = userCreateDto.Email,
                    Password = userCreateDto.Password,
                    Role = userCreateDto.Role
                };
                
                await _userRepo.AddAsync(user);
                return userCreateDto;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create user: {ex.Message}", ex);
            }
        }
    }
}
