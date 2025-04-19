using CineFans.Application.Dtos;


namespace CineFans.Application.Interfaces
{
    public interface IUsersService
    {
        Task<bool> RegisterAsync(UsersRegisterDto dto);
        Task<string?> LoginAsync(UsersLoginDto dto);
        Task<UsersDto?> GetByIdAsync(int id);
        Task<IEnumerable<UsersDto>> GetAllAsync();
        Task<bool> UpdateProfileAsync(int id, UsersDto dto);
    }
}
