using System.Threading.Tasks;
using ShowTime.BusinessLogic.Dtos;

namespace ShowTime.BusinessLogic.Abstractions
{
    public interface IUserService
    {
        Task<UserDto?> LoginAsync(string email, string password);
        Task<UserDto?> GetUserByEmailAsync(string email);
    }
} 