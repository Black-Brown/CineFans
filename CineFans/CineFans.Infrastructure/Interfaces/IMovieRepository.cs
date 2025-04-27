using CineFans.Domain.Entities;

namespace CineFans.Infrastructure.Interface
{
    public interface IMovieRepository
    {
        Task<Movie> GetByIdAsync(int id);
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie> CreateAsync(Movie movie);
        Task<Movie> AddAsync(Movie movie);
        Task<bool> UpdateAsync(Movie movie);
        Task<bool> DeleteAsync(int id);
        Task<string?> GetUserNameByIdAsync(int id);
        Task<List<Movie>> GetMoviesByUserIdAsync(int userId);
        Task<List<Movie>> GetMoviesWithCommentsAsync();  // El método que recupera las películas con comentarios
    }
}
