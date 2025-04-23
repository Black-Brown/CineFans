using CineFans.Application.Contracts;
using CineFans.Common.Dtos;
using CineFans.Common.Requests;
using CineFans.Domain.Entities;
using CineFans.Infrastructure.Interface;

namespace CineFans.Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _repository;

        public GenreService(IGenreRepository repository)
        {
            _repository = repository;
        }

        // Root Cause: The 'Genre' class does not have a property named 'Id'. Instead, it has a property named 'GenreId'.  
        // Solution: Replace all occurrences of 'Id' with 'GenreId' in the affected methods.  

        public async Task<List<GenreDto>> GetAllAsync()
        {
            var genres = await _repository.GetAllAsync();
            return genres.Select(g => new GenreDto
            {
                Id = g.GenreId, // Fixed: Changed 'Id' to 'GenreId'  
                Name = g.Name
            }).ToList();
        }

        public async Task<GenreDto?> GetByIdAsync(int id)
        {
            var genre = await _repository.GetByIdAsync(id);
            if (genre == null) return null;

            return new GenreDto
            {
                Id = genre.GenreId, // Fixed: Changed 'Id' to 'GenreId'  
                Name = genre.Name
            };
        }

        public async Task AddAsync(CreateGenreRequest request)
        {
            var genre = new Genre { Name = request.Name };
            await _repository.AddAsync(genre);
        }

        public async Task UpdateAsync(UpdateGenreRequest request)
        {
            var genre = await _repository.GetByIdAsync(request.Id);
            if (genre != null)
            {
                genre.Name = request.Name;
                await _repository.UpdateAsync(genre);
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
