using System.Collections.Generic;
using System.Threading.Tasks;
using CineFansApp.Domain.Entities;

namespace CineFansApp.Domain.Interfaces
{
    public interface ICommentRepository
    {
        // Operaciones CRUD básicas
        Task<Comment> GetByIdAsync(int comentarioId);
        Task<IEnumerable<Comment>> GetAllAsync();
        Task<Comment> AddAsync(Comment comment);
        Task UpdateAsync(Comment comment);
        Task DeleteAsync(int comentarioId);

        // Consultas específicas
        Task<IEnumerable<Comment>> GetByPostIdAsync(int publicacionId);
        Task<IEnumerable<Comment>> GetByUserIdAsync(int usuarioId);
        Task<IEnumerable<Comment>> GetByPostAndUserIdAsync(int publicacionId, int usuarioId);

        // Métodos con carga de relaciones
        Task<Comment> GetByIdWithPostAsync(int comentarioId);
        Task<Comment> GetByIdWithUserAsync(int comentarioId);
        Task<Comment> GetByIdWithAllRelationsAsync(int comentarioId);
        Task<IEnumerable<Comment>> GetByPostIdWithUserAsync(int publicacionId);
        Task<IEnumerable<Comment>> GetByUserIdWithPostAsync(int usuarioId);
    }
}