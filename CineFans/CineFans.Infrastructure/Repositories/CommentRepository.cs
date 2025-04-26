using CineFans.Domain.Entities;
using CineFans.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace CineFans.Infrastructure.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        private readonly CineFansDbContext _context;

        public CommentRepository(CineFansDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllWithNavigationAsync()
        {
            return await _context.Comments
                .Include(c => c.User)
                .Include(c => c.Movie)
                .ToListAsync();
        }

        public async Task<Comment?> GetByIdWithNavigationAsync(int id)
        {
            return await _context.Comments
                .Include(c => c.User)
                .Include(c => c.Movie)
                .FirstOrDefaultAsync(c => c.CommentId == id);
        }
    }
}
