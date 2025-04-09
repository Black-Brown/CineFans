using CineFansApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CineFansApp.Infrastructure.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<List<User>> GetSuggestedUsersAsync(int userId, int count);
        Task<List<User>> GetFollowersAsync(int userId);
        Task<List<User>> GetFollowingAsync(int userId);
        Task<bool> IsFollowingAsync(int followerId, int followedId);
        Task FollowUserAsync(int followerId, int followedId);
        Task UnfollowUserAsync(int followerId, int followedId);
    }
}
