using CineFans.Domain.Entities;

namespace CineFans.Infrastructure.Interface
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        Task<List<Comment>> GetAllWithNavigationAsync();
        Task<Comment?> GetByIdWithNavigationAsync(int id);
    }
}
