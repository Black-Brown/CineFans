using CineFans.Application.Contracts;
using CineFans.Common.Requests;
using CineFans.Common.Responses;
using CineFans.Domain.Entities;
using CineFans.Infrastructure.Interface;
using System.Security.Cryptography;
using System.Text;

namespace CineFans.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse> CreateUserAsync(CreateUserRequest request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                throw new ArgumentException("Email cannot be null or empty");
            }

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = HashPassword(request.Password),
                RegistrationDate = DateTime.UtcNow
            };

            await _userRepository.CreateAsync(user);

            return new UserResponse
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                RegistrationDate = user.RegistrationDate
            };
        }

        public async Task<LoginResponse> LoginAsync(LoginUserRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
                throw new ArgumentException("Email cannot be null or empty.", nameof(request.Email));

            if (string.IsNullOrWhiteSpace(request.Password))
                throw new ArgumentException("Password cannot be null or empty.", nameof(request.Password));

            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null || !VerifyPassword(request.Password, user.PasswordHash ?? string.Empty))
            {
                return new LoginResponse
                {
                    Success = false,
                    ErrorMessage = "Invalid email or password."
                };
            }

            return new LoginResponse
            {
                Success = true,
                Token = $"fake-token-{user.UserId}" // Puedes integrar JWT después si quieres  
            };
        }

        public async Task<UserResponse?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user == null ? null : MapToResponse(user);
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(MapToResponse);
        }

        public async Task<bool> UpdateUserAsync(UpdateUserRequest request)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null) return false;

            user.Name = request.Name;
            user.Email = request.Email;

            await _userRepository.UpdateAsync(user);
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            await _userRepository.DeleteAsync(user);
            return true;
        }

        public async Task<bool> UserExistsAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return user != null;
        }

        private static UserResponse MapToResponse(User user)
        {
            return new UserResponse
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                RegistrationDate = user.RegistrationDate
            };
        }

        private static string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));

            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        private static bool VerifyPassword(string password, string storedHash)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));

            if (string.IsNullOrWhiteSpace(storedHash))
                throw new ArgumentException("Stored hash cannot be null or empty.", nameof(storedHash));

            return HashPassword(password) == storedHash;
        }
    }
}
