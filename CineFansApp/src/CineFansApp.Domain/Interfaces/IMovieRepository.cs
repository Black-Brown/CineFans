using System.Collections.Generic;
using System.Threading.Tasks;
using CineFansApp.Domain.Entities;

namespace CineFansApp.Domain.Interfaces
{
    public interface IMovieRepository
    {
        // Operaciones CRUD básicas
        Task<Movie> GetByIdAsync(int peliculaId);
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie> AddAsync(Movie movie);
        Task UpdateAsync(Movie movie);
        Task DeleteAsync(int peliculaId);

        // Consultas específicas
        Task<IEnumerable<Movie>> GetByGenreIdAsync(int generoId);
        Task<IEnumerable<Movie>> GetByYearAsync(int anio);
        Task<IEnumerable<Movie>> GetByDirectorAsync(string director);
        Task<IEnumerable<Movie>> SearchByTitleAsync(string titulo);

        // Métodos con carga de relaciones
        Task<Movie> GetByIdWithPostsAsync(int peliculaId);
        Task<Movie> GetByIdWithGenreAsync(int peliculaId);
        Task<Movie> GetByIdWithAllRelationsAsync(int peliculaId);
        Task<IEnumerable<Movie>> GetAllWithGenreAsync();
        Task<IEnumerable<Movie>> GetByGenreIdWithPostsAsync(int generoId);
        Task<IEnumerable<Movie>> SearchByTitleWithGenreAsync(string titulo);
    }
}