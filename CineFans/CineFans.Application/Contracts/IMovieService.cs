using CineFans.Common.Dtos;
using CineFans.Common.Requests;

namespace CineFans.Application.Contracts
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDto>> GetAllAsync();
        Task<MovieDto?> GetByIdAsync(int id);
        Task<MovieDto> CreateAsync(CreateMovieRequest request, string webRootPath);
        Task<MovieDto?> UpdateAsync(UpdateMovieRequest request, string webRootPath);
        Task<bool> DeleteAsync(int id);
    }
}
