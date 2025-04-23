using CineFans.Domain.Entities;

namespace CineFans.Infrastructure.Interface
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User?> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}
