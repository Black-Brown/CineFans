using CineFansApp.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CineFansApp.Application.Interfaces
{
    public interface IPostService
    {
        Task<PostDto> GetPostByIdAsync(int postId);
        Task<List<PostDto>> GetAllPostsAsync();
        Task<List<PostDto>> GetRecentPostsAsync(int count);
        Task<List<PostDto>> GetFeedPostsAsync(int userId);
        Task<List<PostDto>> GetPostsByUserIdAsync(int userId);
        Task<List<PostDto>> GetPostsByMovieIdAsync(int movieId);
        Task<PostDto> CreatePostAsync(PostDto postDto);
        Task<PostDto> UpdatePostAsync(PostDto postDto);
        Task DeletePostAsync(int postId);
        Task LikePostAsync(int postId, int userId);
        Task UnlikePostAsync(int postId, int userId);
    }
}
