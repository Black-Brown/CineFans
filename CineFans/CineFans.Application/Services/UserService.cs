using CineFans.Application.Contracts;
using CineFans.Common.Requests;
using CineFans.Common.Responses;
using CineFans.Domain.Entities;
using CineFans.Infrastructure.Interface;

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
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                ProfilePicture = request.ProfilePicture ?? string.Empty, // Asigna la URL de la imagen aquí
                RegistrationDate = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);

            return new UserResponse
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                ProfilePicture = user.ProfilePicture,
                RegistrationDate = user.RegistrationDate
            };
        }

        public async Task<UserResponse?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            return user == null ? null : new UserResponse
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                ProfilePicture = user.ProfilePicture,
                RegistrationDate = user.RegistrationDate
            };
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return users.Select(user => new UserResponse
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                ProfilePicture = user.ProfilePicture,
                RegistrationDate = user.RegistrationDate
            });
        }

        public async Task<bool> UpdateUserAsync(UpdateUserRequest request)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null) return false;

            user.Name = request.Name;
            user.Email = request.Email;
            user.PasswordHash = request.PasswordHash;
            user.ProfilePicture = request.ProfilePicture ?? user.ProfilePicture; // Handle possible null value  
            user.RegistrationDate = request.RegistrationDate;

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
    }
}
