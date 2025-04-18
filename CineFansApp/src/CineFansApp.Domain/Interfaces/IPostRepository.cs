using System.Collections.Generic;
using System.Threading.Tasks;
using CineFansApp.Domain.Entities;

namespace CineFansApp.Domain.Interfaces
{
    public interface IPostRepository
    {
        // Operaciones básicas CRUD
        Task<Post> GetByIdAsync(int publicacionId);
        Task<IEnumerable<Post>> GetAllAsync();
        Task<Post> AddAsync(Post post);
        Task UpdateAsync(Post post);
        Task DeleteAsync(int publicacionId);

        // Consultas específicas
        Task<IEnumerable<Post>> GetByUserIdAsync(int usuarioId);
        Task<IEnumerable<Post>> GetByMovieIdAsync(int peliculaId);

        // Métodos con includes para relaciones
        Task<Post> GetByIdWithCommentsAsync(int publicacionId);
        Task<Post> GetByIdWithLikesAsync(int publicacionId);
        Task<Post> GetByIdWithAllRelationsAsync(int publicacionId);
    }
}