using System.Threading.Tasks;
using ShowTime.BusinessLogic.Dtos;

namespace ShowTime.BusinessLogic.Abstractions
{
    public interface IUserService
    {
        Task<LoginResponseDto?> LoginAsync(LoginDto login);
        //Task<LoginDto?> GetUserByEmailAsync(string email);
        // adaugare in baza si hash-uire de parole
    }
} 