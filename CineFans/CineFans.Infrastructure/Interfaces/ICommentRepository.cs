using CineFans.Domain.Entities;

namespace CineFans.Infrastructure.Interfaces
{
    public interface ICommentRepository
    {
        Task AddAsync(Comment comment);
        Task<List<Comment>> GetByMovieIdAsync(int movieId);
        Task<List<Comment>> GetByUserIdAsync(int userId);
    }
}
