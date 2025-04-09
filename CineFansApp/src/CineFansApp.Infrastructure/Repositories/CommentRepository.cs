using CineFansApp.Domain.Entities;
using CineFansApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineFansApp.Infrastructure.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<Comment> GetByIdAsync(int id)
        {
            return await _context.Comments
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.ComentarioId == id);
        }

        public async Task<List<Comment>> GetCommentsByPostIdAsync(int postId)
        {
            return await _context.Comments
                .Include(c => c.User)
                .Where(c => c.PublicacionId == postId)
                .OrderBy(c => c.Fecha)
                .ToListAsync();
        }
    }
}
