using CineFansApp.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CineFansApp.Application.Interfaces
{
    public interface IGenreService
    {
        Task<GenreDto> GetGenreByIdAsync(int genreId);
        Task<List<GenreDto>> GetAllGenresAsync();
        Task<GenreDto> CreateGenreAsync(GenreDto genreDto);
        Task<GenreDto> UpdateGenreAsync(GenreDto genreDto);
        Task DeleteGenreAsync(int genreId);
    }
}
