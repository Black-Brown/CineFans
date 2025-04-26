using CineFans.Common.Requests;
using CineFans.Common.Responses;

namespace CineFans.Application.Contracts
{
    public interface IUserService
    {
        Task<UserResponse> CreateUserAsync(CreateUserRequest request);
        Task<UserResponse?> GetUserByIdAsync(int id);
        Task<IEnumerable<UserResponse>> GetAllUsersAsync();
        Task<bool> UpdateUserAsync(UpdateUserRequest request);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> UserExistsAsync(int userId);
    }
}
