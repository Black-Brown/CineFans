using CineFans.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CineFans.Infrastructure.Interface
{
    public interface ICommentRepository
    {
        Task<Comment?> GetByIdAsync(int commentId);
        Task<List<Comment>> GetAllAsync();
        Task<List<Comment>> GetCommentsByMovieIdAsync(int movieId);
        Task<Comment> AddAsync(Comment comment);
        Task<List<Comment>> GetCommentsByUserIdAsync(int userId);
    }
}
