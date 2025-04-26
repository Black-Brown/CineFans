using CineFans.Domain.Entities;
using CineFans.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace CineFans.Infrastructure.Repositories
{
    // The issue is that there are two methods with the same name and parameter types in the UserRepository class.  
    // Specifically, there are two definitions of the method `GetByIdAsync(int id)`.  
    // This causes a CS0111 compiler error because method names and parameter types must be unique within a class.  

    // To fix this, remove one of the duplicate methods. Here's the corrected code:  

    public class UserRepository : IUserRepository
    {
        private readonly CineFansDbContext _context;

        public UserRepository(CineFansDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<string?> GetUserNameByIdAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return user?.Name ?? "Unknown User";
        }
    }
}
