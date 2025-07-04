using System.Threading.Tasks;
using ShowTime.BusinessLogic.Abstractions;
using ShowTime.BusinessLogic.Dtos;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace ShowTime.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly ShowTimeContext _context;
        public UserService(ShowTimeContext context)
        {
            _context = context;
        }

        public async Task<UserDto?> LoginAsync(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            if (user == null) return null;
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task<UserDto?> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return null;
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role
            };
        }
    }
} 