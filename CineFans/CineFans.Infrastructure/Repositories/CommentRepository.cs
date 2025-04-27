using CineFans.Domain.Entities;
using CineFans.Infrastructure.Context;
using CineFans.Infrastructure.Interface;
using CineFans.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CineFans.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly CineFansDbContext _context;

        public CommentRepository(CineFansDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Comment>> GetByMovieIdAsync(int movieId)
        {
            return await _context.Comments
                .Where(c => c.MovieId == movieId)
                .ToListAsync();
        }

        public async Task<List<Comment>> GetByUserIdAsync(int userId)
        {
            return await _context.Comments
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }
    }
}
