using CineFansApp.Domain.Entities;
using CineFansApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineFansApp.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<List<User>> GetSuggestedUsersAsync(int userId, int count)
        {
            // Obtener IDs de usuarios que el usuario actual sigue
            var followingIds = await _context.Follows
                .Where(f => f.SeguidorId == userId)
                .Select(f => f.SeguidoId)
                .ToListAsync();

            // Añadir el ID del usuario actual para excluirlo también
            followingIds.Add(userId);

            // Obtener usuarios sugeridos (que no siga el usuario actual)
            return await _context.Users
                .Where(u => !followingIds.Contains(u.UserId))
                .OrderBy(u => Guid.NewGuid()) // Ordenar aleatoriamente
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<User>> GetFollowersAsync(int userId)
        {
            return await _context.Follows
                .Where(f => f.SeguidoId == userId)
                .Include(f => f.Follower)
                .Select(f => f.Follower)
                .ToListAsync();
        }

        public async Task<List<User>> GetFollowingAsync(int userId)
        {
            return await _context.Follows
                .Where(f => f.SeguidorId == userId)
                .Include(f => f.Following)
                .Select(f => f.Following)
                .ToListAsync();
        }

        public async Task<bool> IsFollowingAsync(int followerId, int followedId)
        {
            return await _context.Follows
                .AnyAsync(f => f.SeguidorId == followerId && f.SeguidoId == followedId);
        }

        public async Task FollowUserAsync(int followerId, int followedId)
        {
            var follow = new Follow
            {
                SeguidorId = followerId,
                SeguidoId = followedId
            };

            await _context.Follows.AddAsync(follow);
        }

        public async Task UnfollowUserAsync(int followerId, int followedId)
        {
            var follow = await _context.Follows
                .FirstOrDefaultAsync(f => f.SeguidorId == followerId && f.SeguidoId == followedId);

            if (follow != null)
            {
                _context.Follows.Remove(follow);
            }
        }
    }
}
