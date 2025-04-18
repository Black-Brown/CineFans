using System.Collections.Generic;
using System.Threading.Tasks;
using CineFansApp.Domain.Entities;

namespace CineFansApp.Domain.Interfaces
{
    public interface IFollowRepository
    {
        // Operaciones básicas con clave compuesta
        Task<Follow> GetByIdAsync(int seguidorId, int seguidoId);
        Task<IEnumerable<Follow>> GetAllAsync();
        Task<Follow> AddAsync(Follow follow);
        Task UpdateAsync(Follow follow);
        Task DeleteAsync(int seguidorId, int seguidoId);

        // Consultas específicas de relaciones
        Task<IEnumerable<Follow>> GetFollowersByUserIdAsync(int seguidoId);
        Task<IEnumerable<Follow>> GetFollowingByUserIdAsync(int seguidorId);
        Task<int> GetFollowerCountAsync(int seguidoId);
        Task<int> GetFollowingCountAsync(int seguidorId);
        Task<bool> ExistsAsync(int seguidorId, int seguidoId);

        // Métodos con carga de relaciones
        Task<Follow> GetByIdWithFollowerAsync(int seguidorId, int seguidoId);
        Task<Follow> GetByIdWithFollowingAsync(int seguidorId, int seguidoId);
        Task<Follow> GetByIdWithAllRelationsAsync(int seguidorId, int seguidoId);
        Task<IEnumerable<Follow>> GetFollowersByUserIdWithDetailsAsync(int seguidoId);
        Task<IEnumerable<Follow>> GetFollowingByUserIdWithDetailsAsync(int seguidorId);
    }
}