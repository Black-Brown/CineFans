using CineFansApp.Domain.DTOs;
using System.Threading.Tasks;

namespace CineFansApp.Application.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResult<UserDto>> ValidateUserAsync(string email, string password);
        Task<ServiceResult<UserDto>> RegisterUserAsync(UserDto userDto, string password);
        Task<string> AuthenticateAsync(string? email, string? password);
    }
}
