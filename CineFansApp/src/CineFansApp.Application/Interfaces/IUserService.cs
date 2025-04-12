using CineFansApp.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CineFansApp.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto?> GetUserByIdAsync(int userId);
        Task<UserDto?> GetUserByEmailAsync(string email);
        Task<List<UserDto>> GetAllUsersAsync();
        Task<List<UserDto>> GetSuggestedUsersAsync(int userId, int count);
        Task<List<UserDto?>> GetFollowersAsync(int userId);
        Task<List<UserDto?>> GetFollowingAsync(int userId);
        Task<bool> IsFollowingAsync(int followerId, int followedId);
        Task FollowUserAsync(int followerId, int followedId);
        Task UnfollowUserAsync(int followerId, int followedId);
        Task<UserDto?> UpdateUserAsync(UserDto userDto);
    }
}
