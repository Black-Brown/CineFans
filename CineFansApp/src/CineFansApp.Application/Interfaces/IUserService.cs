using CineFansApp.Application.DTOs;

namespace CineFansApp.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(UserRegisterDto dto);
        Task<string?> LoginAsync(UserLoginDto dto);
        Task<UserDto?> GetByIdAsync(int id);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<bool> UpdateProfileAsync(int id, UserDto dto);
    }
}
