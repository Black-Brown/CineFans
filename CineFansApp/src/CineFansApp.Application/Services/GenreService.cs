using CineFansApp.Application.Interfaces;
using CineFansApp.Domain.DTOs;
using CineFansApp.Domain.Entities;
using CineFansApp.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineFansApp.Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GenreService(IGenreRepository genreRepository, IUnitOfWork unitOfWork)
        {
            _genreRepository = genreRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<GenreDto> GetGenreByIdAsync(int genreId)
        {
            var genre = await _genreRepository.GetByIdAsync(genreId);
            if (genre == null)
                return null;

            return MapToDto(genre);
        }

        public async Task<List<GenreDto>> GetAllGenresAsync()
        {
            var genres = await _genreRepository.GetAllAsync();
            return genres.Select(MapToDto).ToList();
        }

        public async Task<GenreDto> CreateGenreAsync(GenreDto genreDto)
        {
            var genre = new Genre
            {
                Nombre = genreDto.Nombre
            };

            _genreRepository.Add(genre);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(genre);
        }

        public async Task<GenreDto> UpdateGenreAsync(GenreDto genreDto)
        {
            var genre = await _genreRepository.GetByIdAsync(genreDto.GeneroId);
            if (genre == null)
                throw new KeyNotFoundException($"Género con ID {genreDto.GeneroId} no encontrado");

            genre.Nombre = genreDto.Nombre;

            await _unitOfWork.SaveChangesAsync();
            return MapToDto(genre);
        }

        public async Task DeleteGenreAsync(int genreId)
        {
            var genre = await _genreRepository.GetByIdAsync(genreId);
            if (genre == null)
                throw new KeyNotFoundException($"Género con ID {genreId} no encontrado");

            _genreRepository.Remove(genre);
            await _unitOfWork.SaveChangesAsync();
        }

        private GenreDto MapToDto(Genre genre)
        {
            return new GenreDto
            {
                GeneroId = genre.GeneroId,
                Nombre = genre.Nombre
            };
        }
    }
}
