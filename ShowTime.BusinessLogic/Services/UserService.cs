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

        //public async Task<LoginResponseDto?> LoginAsync(LoginDto login)
        //{
        //    var user = await _userRepo.GetAllAsync(u => u.Email == login.Email && u.Password == login.Password);

        //    if (user != null && user.Any())
        //    {
        //        var matchedUser = user.First();
        //        return new LoginResponseDto
        //        {
        //            Role = matchedUser.Role
        //        };
        //    }

        //    return null;
        //}

        public async Task<LoginResponseDto?> LoginAsync(LoginDto login)
        {
            return new LoginResponseDto
            {

                Role = 0
            };
            }
        }


        //public async Task<LoginDto?> GetUserByEmailAsync(string email)
        //{
        //    var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        //    if (user == null) return null;
        //    return new LoginDto
        //    {
        //        Id = user.Id,
        //        Email = user.Email,
        //        Role = user.Role
        //    };
        //}
    }
