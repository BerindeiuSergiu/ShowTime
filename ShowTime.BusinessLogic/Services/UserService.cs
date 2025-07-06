using System.Threading.Tasks;
using ShowTime.BusinessLogic.Abstractions;
using ShowTime.BusinessLogic.Dtos;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess;
using Microsoft.EntityFrameworkCore;
using ShowTime.DataAccess.GenericInterface;
using System.Security.Cryptography;
using System.Text;

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
                var user = users.FirstOrDefault(u => u.Email == login.Email);

                if (user == null)
                {
                    return null;
                }

                // Verify the password hash
                if (!VerifyPassword(login.Password, user.Password))
                {
                    return null;
                }

                return new LoginResponseDto
                {
                    Role = user.Role
                };
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return null;
            }
        }

        public async Task<UserCreateDto> CreateUserAsync(UserCreateDto userCreateDto)
        {
            try
            {
                // verif existing user
                var users = await _userRepo.GetAllAsync();
                var existingUser = users.FirstOrDefault(u => u.Email == userCreateDto.Email);

                if (existingUser != null)
                {
                    throw new InvalidOperationException("A user with this email already exists.");
                }

                // hash pe parola
                var hashedPassword = HashPassword(userCreateDto.Password);

                var user = new User
                {
                    Email = userCreateDto.Email,
                    Password = hashedPassword, 
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

        private string HashPassword(string password)
        {
            //ranodm salt
            using var rng = RandomNumberGenerator.Create();
            var salt = new byte[32]; // 256 bits
            rng.GetBytes(salt);

            // hashing the password
            using var sha256 = SHA256.Create();
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var passwordWithSalt = new byte[passwordBytes.Length + salt.Length];

            Array.Copy(passwordBytes, 0, passwordWithSalt, 0, passwordBytes.Length);
            Array.Copy(salt, 0, passwordWithSalt, passwordBytes.Length, salt.Length);

            var hash = sha256.ComputeHash(passwordWithSalt);

            // combine salt and hash
            var hashWithSalt = new byte[salt.Length + hash.Length];
            Array.Copy(salt, 0, hashWithSalt, 0, salt.Length);
            Array.Copy(hash, 0, hashWithSalt, salt.Length, hash.Length);

            return Convert.ToBase64String(hashWithSalt);
        }


        private bool VerifyPassword(string password, string storedHash)
        {
            try
            {
                var hashWithSalt = Convert.FromBase64String(storedHash);

                // extract salt (first 32 bytes)
                var salt = new byte[32];
                Array.Copy(hashWithSalt, 0, salt, 0, 32);

                // extract hash (remaining bytes)
                var storedHashBytes = new byte[hashWithSalt.Length - 32];
                Array.Copy(hashWithSalt, 32, storedHashBytes, 0, storedHashBytes.Length);

                // hash pass with salt
                using var sha256 = SHA256.Create();
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var passwordWithSalt = new byte[passwordBytes.Length + salt.Length];

                Array.Copy(passwordBytes, 0, passwordWithSalt, 0, passwordBytes.Length);
                Array.Copy(salt, 0, passwordWithSalt, passwordBytes.Length, salt.Length);

                var computedHash = sha256.ComputeHash(passwordWithSalt);

                // compare stored hash with computed hash
                return computedHash.SequenceEqual(storedHashBytes);
            }
            catch
            {
                return false;
            }
        }
    }
}