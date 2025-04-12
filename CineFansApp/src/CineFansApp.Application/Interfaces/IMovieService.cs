using CineFansApp.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CineFansApp.Application.Interfaces
{
    public interface IMovieService
    {
        Task<MovieDto?> GetMovieByIdAsync(int movieId);
        Task<List<MovieDto>> GetAllMoviesAsync();
        Task<List<MovieDto>> GetPopularMoviesAsync(int count);
        Task<MovieDto?> CreateMovieAsync(MovieDto movieDto);
        Task<MovieDto?> UpdateMovieAsync(MovieDto movieDto);
        Task DeleteMovieAsync(int movieId);
    }
}
