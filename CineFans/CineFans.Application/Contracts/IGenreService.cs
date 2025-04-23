using CineFans.Common.Dtos;
using CineFans.Common.Requests;

namespace CineFans.Application.Contracts
{
    public interface IGenreService
    {
        Task<List<GenreDto>> GetAllAsync();
        Task<GenreDto?> GetByIdAsync(int id);
        Task AddAsync(CreateGenreRequest request);
        Task UpdateAsync(UpdateGenreRequest request);
        Task DeleteAsync(int id);
    }
}
