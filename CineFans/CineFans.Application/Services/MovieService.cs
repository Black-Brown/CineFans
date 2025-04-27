using CineFans.Application.Contracts;
using CineFans.Common.Dtos;
using CineFans.Common.Requests;
using CineFans.Common.Responses;
using CineFans.Domain.Entities;
using CineFans.Infrastructure.Interface;

namespace CineFans.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository; // Asumiendo que tienes un repositorio para manejar las películas

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        // Crear una película
        public async Task<MovieResponse> CreateAsync(CreateMovieRequest request)
        {
            // Crear la entidad Movie a partir del request
            var movie = new Movie
            {
                Title = request.Title,
                Description = request.Description,
                Year = request.Year,
                Director = request.Director,
                GenreName = request.GenreName,
                ImageUrl = request.ImageUrl // Si la imagen se proporciona
            };

            // Guardar la película en la base de datos
            var createdMovie = await _movieRepository.AddAsync(movie);

            // Convertir la entidad Movie en MovieResponse para devolverla
            return new MovieResponse
            {
                MovieId = createdMovie.MovieId,
                Title = createdMovie.Title,
                Description = createdMovie.Description,
                Year = createdMovie.Year,
                Director = createdMovie.Director,
                GenreName = createdMovie.GenreName,
                ImageUrl = createdMovie.ImageUrl
            };
        }

        // Obtener una película por ID
        public async Task<MovieResponse?> GetByIdAsync(int id)
        {
            // Obtener la película desde el repositorio
            var movie = await _movieRepository.GetByIdAsync(id);

            if (movie == null)
                return null;

            // Convertir la entidad Movie a MovieResponse
            return new MovieResponse
            {
                MovieId = movie.MovieId,
                Title = movie.Title,
                Description = movie.Description,
                Year = movie.Year,
                Director = movie.Director,
                GenreName = movie.GenreName,
                ImageUrl = movie.ImageUrl
            };
        }

        // Obtener todas las películas
        public async Task<IEnumerable<MovieResponse>> GetAllAsync()
        {
            // Obtener todas las películas desde el repositorio
            var movies = await _movieRepository.GetAllAsync();

            // Convertir las entidades Movie a MovieResponse
            return movies.Select(movie => new MovieResponse
            {
                MovieId = movie.MovieId,
                Title = movie.Title,
                Description = movie.Description,
                Year = movie.Year,
                Director = movie.Director,
                GenreName = movie.GenreName,
                ImageUrl = movie.ImageUrl
            });
        }

        // Actualizar una película
        public async Task<bool> UpdateAsync(UpdateMovieRequest request)
        {
            // Obtener la película existente
            var movie = await _movieRepository.GetByIdAsync(request.MovieId);

            if (movie == null)
                return false; // La película no existe

            // Actualizar la película con los nuevos datos
            movie.Title = request.Title;
            movie.Description = request.Description;
            movie.Year = request.Year;
            movie.Director = request.Director;
            movie.GenreName = request.GenreName;
            movie.ImageUrl = request.ImageUrl;

            // Guardar los cambios
            await _movieRepository.UpdateAsync(movie);

            return true;
        }

        // Eliminar una película
        public async Task<bool> DeleteAsync(int id)
        {
            // Obtener la película a eliminar  
            var movie = await _movieRepository.GetByIdAsync(id);

            if (movie == null)
                return false; // La película no existe  

            // Eliminar la película usando el ID en lugar de la entidad  
            await _movieRepository.DeleteAsync(movie.MovieId);

            return true;
        }

        public async Task<bool> MovieExistsAsync(int movieId)
        {
            var movie = await _movieRepository.GetByIdAsync(movieId);
            return movie != null;
        }

        public async Task<List<MovieDto>> GetMoviesWithCommentsAsync()
        {
            // Obtener películas con comentarios
            var movies = await _movieRepository.GetMoviesWithCommentsAsync();

            // Mapear las entidades Movie a MovieDto
            var movieDtos = movies.Select(m => new MovieDto
            {
                MovieId = m.MovieId,
                Title = m.Title,
                Description = m.Description,
                Year = m.Year,
                Director = m.Director,
                GenreName = m.GenreName,  // Corregido para usar GenreName
                ImageUrl = m.ImageUrl,
                Comments = m.Comments.Select(c => new CommentDto
                {
                    CommentId = c.CommentId,
                    MovieId = c.MovieId,
                    UserId = c.UserId,
                    Text = c.Text,
                    Date = c.Date
                }).ToList()
            }).ToList();

            return movieDtos;
        }
    }
}
