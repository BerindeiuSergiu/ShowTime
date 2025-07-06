using System.Threading.Tasks;
using ShowTime.BusinessLogic.Dtos;

namespace ShowTime.BusinessLogic.Abstractions
{
    public interface IUserService
    {
        Task<LoginResponseDto?> LoginAsync(LoginDto login);
        Task<UserCreateDto> CreateUserAsync(UserCreateDto userCreateDto);
        //Task<LoginDto?> GetUserByEmailAsync(string email);
        // adaugare in baza si hash-uire de parole
    }
} 