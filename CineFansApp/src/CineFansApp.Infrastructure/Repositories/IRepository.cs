using System.Collections.Generic;
using System.Threading.Tasks;

namespace CineFansApp.Infrastructure.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
