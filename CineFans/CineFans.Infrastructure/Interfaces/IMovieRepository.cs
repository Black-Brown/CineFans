using CineFans.Domain.Entities;

namespace CineFans.Infrastructure.Interface
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetAllAsync();
        Task<Movie?> GetByIdAsync(int id);
        Task AddAsync(Movie movie);
        Task UpdateAsync(Movie movie);
        Task<bool> DeleteAsync(int id);
    }
}
