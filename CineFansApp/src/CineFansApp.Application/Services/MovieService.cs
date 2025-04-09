using CineFansApp.Application.Interfaces;
using CineFansApp.Domain.DTOs;
using CineFansApp.Domain.Entities;
using CineFansApp.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineFansApp.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MovieService(IMovieRepository movieRepository, IUnitOfWork unitOfWork)
        {
            _movieRepository = movieRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<MovieDto> GetMovieByIdAsync(int movieId)
        {
            var movie = await _movieRepository.GetByIdAsync(movieId);
            if (movie == null)
                return null;

            return MapToDto(movie);
        }

        public async Task<List<MovieDto>> GetAllMoviesAsync()
        {
            var movies = await _movieRepository.GetAllAsync();
            return movies.Select(MapToDto).ToList();
        }

        public async Task<List<MovieDto>> GetPopularMoviesAsync(int count)
        {
            var movies = await _movieRepository.GetPopularMoviesAsync(count);
            return movies.Select(MapToDto).ToList();
        }

        public async Task<MovieDto> CreateMovieAsync(MovieDto movieDto)
        {
            var movie = new Movie
            {
                Titulo = movieDto.Titulo,
                Descripcion = movieDto.Descripcion,
                Anio = movieDto.Anio,
                Director = movieDto.Director,
                GeneroId = movieDto.GeneroId,
                ImagenUrl = movieDto.ImagenUrl
            };

            _movieRepository.Add(movie);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(movie);
        }

        public async Task<MovieDto> UpdateMovieAsync(MovieDto movieDto)
        {
            var movie = await _movieRepository.GetByIdAsync(movieDto.PeliculaId);
            if (movie == null)
                throw new KeyNotFoundException($"Película con ID {movieDto.PeliculaId} no encontrada");

            movie.Titulo = movieDto.Titulo;
            movie.Descripcion = movieDto.Descripcion;
            movie.Anio = movieDto.Anio;
            movie.Director = movieDto.Director;
            movie.GeneroId = movieDto.GeneroId;
            movie.ImagenUrl = movieDto.ImagenUrl;

            await _unitOfWork.SaveChangesAsync();
            return MapToDto(movie);
        }

        public async Task DeleteMovieAsync(int movieId)
        {
            var movie = await _movieRepository.GetByIdAsync(movieId);
            if (movie == null)
                throw new KeyNotFoundException($"Película con ID {movieId} no encontrada");

            _movieRepository.Remove(movie);
            await _unitOfWork.SaveChangesAsync();
        }

        private MovieDto MapToDto(Movie movie)
        {
            return new MovieDto
            {
                PeliculaId = movie.PeliculaId,
                Titulo = movie.Titulo,
                Descripcion = movie.Descripcion,
                Anio = movie.Anio,
                Director = movie.Director,
                GeneroId = movie.GeneroId,
                GeneroNombre = movie.Genre?.Nombre,
                ImagenUrl = movie.ImagenUrl
            };
        }
    }
}
