using CineFansApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CineFansApp.Infrastructure.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<List<Comment>> GetCommentsByPostIdAsync(int postId);
    }
}
