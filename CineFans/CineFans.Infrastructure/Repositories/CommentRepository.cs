using CineFans.Domain.Entities;
using CineFans.Infrastructure.Context;
using CineFans.Infrastructure.Interface; // Asegúrate de que tienes esta interfaz
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CineFans.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly CineFansDbContext _context;

        public CommentRepository(CineFansDbContext context)
        {
            _context = context;
        }

        public async Task<Comment?> GetByIdAsync(int commentId)
        {
            return await _context.Comments
                .FirstOrDefaultAsync(c => c.CommentId == commentId);
        }

        public async Task<List<Comment>> GetCommentsByMovieIdAsync(int movieId)
        {
            return await _context.Comments
                .Where(c => c.MovieId == movieId)
                .ToListAsync();
        }

        public async Task<List<Comment>> GetCommentsByUserIdAsync(int userId)
        {
            return await _context.Comments
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task<Comment> AddAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment; // Ensure the method returns the added comment as required by the interface  
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }
    }
}
