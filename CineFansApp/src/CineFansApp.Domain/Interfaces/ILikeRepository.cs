using System.Collections.Generic;
using System.Threading.Tasks;
using CineFansApp.Domain.Entities;

namespace CineFansApp.Domain.Interfaces
{
    public interface ILikeRepository
    {
        // Operaciones básicas
        Task<Like> GetByIdAsync(int publicacionId, int usuarioId);
        Task<IEnumerable<Like>> GetAllAsync();
        Task<Like> AddAsync(Like like);
        Task UpdateAsync(Like like);
        Task DeleteAsync(int publicacionId, int usuarioId);

        // Consultas específicas
        Task<IEnumerable<Like>> GetByPostIdAsync(int publicacionId);
        Task<IEnumerable<Like>> GetByUserIdAsync(int usuarioId);
        Task<int> GetLikeCountForPostAsync(int publicacionId);
        Task<bool> ExistsAsync(int publicacionId, int usuarioId);

        // Métodos con relaciones
        Task<Like> GetByIdWithPostAsync(int publicacionId, int usuarioId);
        Task<Like> GetByIdWithUserAsync(int publicacionId, int usuarioId);
        Task<Like> GetByIdWithAllRelationsAsync(int publicacionId, int usuarioId);
        Task<IEnumerable<Like>> GetByPostIdWithUsersAsync(int publicacionId);
        Task<IEnumerable<Like>> GetByUserIdWithPostsAsync(int usuarioId);
    }
}