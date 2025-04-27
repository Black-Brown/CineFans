using CineFans.Common.Dtos;
using CineFans.Common.Requests;
using CineFans.Common.Responses;

namespace CineFans.Application.Contracts
{
    public interface IMovieService
    {
        Task<MovieResponse> CreateAsync(CreateMovieRequest request); // Crear una película
        Task<MovieResponse?> GetByIdAsync(int id); // Obtener película por ID
        Task<IEnumerable<MovieResponse>> GetAllAsync(); // Obtener todas las películas
        Task<bool> UpdateAsync(UpdateMovieRequest request); // Actualizar una película
        Task<bool> DeleteAsync(int id); // Eliminar una película
        Task<bool> MovieExistsAsync(int movieId);
        Task<List<MovieDto>> GetMoviesWithCommentsAsync();
    }
}
