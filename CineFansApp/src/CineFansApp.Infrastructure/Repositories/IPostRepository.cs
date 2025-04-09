using CineFansApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CineFansApp.Infrastructure.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<List<Post>> GetRecentPostsAsync(int count);
        Task<List<Post>> GetFeedPostsAsync(int userId);
        Task<List<Post>> GetPostsByUserIdAsync(int userId);
        Task<List<Post>> GetPostsByMovieIdAsync(int movieId);
        Task<bool> HasUserLikedPostAsync(int postId, int userId);
        Task AddLikeAsync(int postId, int userId);
        Task RemoveLikeAsync(int postId, int userId);
    }
}
