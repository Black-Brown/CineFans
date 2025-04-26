// En CineFans.Infrastructure.Interface
using CineFans.Domain.Entities;

public interface IMovieRepository
{
    Task<Movie> GetByIdAsync(int id);
    Task<IEnumerable<Movie>> GetAllAsync();
    Task<Movie> CreateAsync(Movie movie);
    Task<Movie> AddAsync(Movie movie); // Add this method to resolve the issue
    Task<bool> UpdateAsync(Movie movie);
    Task<bool> DeleteAsync(int id);
    Task<string?> GetUserNameByIdAsync(int id);

}
