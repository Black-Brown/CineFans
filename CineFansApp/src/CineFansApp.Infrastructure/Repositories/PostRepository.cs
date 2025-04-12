using CineFansApp.Domain.Entities;
using CineFansApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineFansApp.Infrastructure.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<Post?> GetByIdAsync(int id)
        {
            return await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Movie)
                .Include(p => p.Likes)
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.PublicacionId == id);
        }

        public override async Task<List<Post>> GetAllAsync()
        {
            return await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Movie)
                .Include(p => p.Likes)
                .Include(p => p.Comments)
                .OrderByDescending(p => p.Fecha)
                .ToListAsync();
        }

        public async Task<List<Post>> GetRecentPostsAsync(int count)
        {
            return await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Movie)
                .Include(p => p.Likes)
                .Include(p => p.Comments)
                .OrderByDescending(p => p.Fecha)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Post>> GetFeedPostsAsync(int userId)
        {
            // Obtener IDs de usuarios que el usuario sigue
            var followingIds = await _context.Follows
                .Where(f => f.SeguidorId == userId)
                .Select(f => f.SeguidoId)
                .ToListAsync();

            // Añadir el ID del propio usuario
            followingIds.Add(userId);

            // Obtener publicaciones de los usuarios seguidos y propias
            return await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Movie)
                .Include(p => p.Likes)
                .Include(p => p.Comments)
                .Where(p => followingIds.Contains(p.UsuarioId))
                .OrderByDescending(p => p.Fecha)
                .ToListAsync();
        }

        public async Task<List<Post>> GetPostsByUserIdAsync(int userId)
        {
            return await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Movie)
                .Include(p => p.Likes)
                .Include(p => p.Comments)
                .Where(p => p.UsuarioId == userId)
                .OrderByDescending(p => p.Fecha)
                .ToListAsync();
        }

        public async Task<List<Post>> GetPostsByMovieIdAsync(int movieId)
        {
            return await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Movie)
                .Include(p => p.Likes)
                .Include(p => p.Comments)
                .Where(p => p.PeliculaId == movieId)
                .OrderByDescending(p => p.Fecha)
                .ToListAsync();
        }

        public async Task<bool> HasUserLikedPostAsync(int postId, int userId)
        {
            return await _context.Likes
                .AnyAsync(l => l.PublicacionId == postId && l.UsuarioId == userId);
        }

        public async Task AddLikeAsync(int postId, int userId)
        {
            var like = new Like
            {
                PublicacionId = postId,
                UsuarioId = userId
            };

            await _context.Likes.AddAsync(like);
        }

        public async Task RemoveLikeAsync(int postId, int userId)
        {
            var like = await _context.Likes
                .FirstOrDefaultAsync(l => l.PublicacionId == postId && l.UsuarioId == userId);

            if (like != null)
            {
                _context.Likes.Remove(like);
            }
        }
    }
}
