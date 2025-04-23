using CineFans.Application.Contracts;
using CineFans.Common.Dtos;
using CineFans.Common.Requests;
using CineFans.Domain.Entities;
using CineFans.Infrastructure.Interface;
using System.IO;

namespace CineFans.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<IEnumerable<MovieDto>> GetAllAsync()
        {
            var movies = await _movieRepository.GetAllAsync();
            return movies.Select(m => new MovieDto
            {
                MovieId = m.MovieId,
                Title = m.Title,
                Description = m.Description,
                Year = m.Year,
                Director = m.Director,
                GenreId = m.GenreId,
                ImageUrl = m.ImageUrl,
                GenreName = m.Genre?.Name ?? ""
            });
        }

        public async Task<MovieDto?> GetByIdAsync(int id)
        {
            var m = await _movieRepository.GetByIdAsync(id);
            if (m == null) return null;

            return new MovieDto
            {
                MovieId = m.MovieId,
                Title = m.Title,
                Description = m.Description,
                Year = m.Year,
                Director = m.Director,
                GenreId = m.GenreId,
                ImageUrl = m.ImageUrl,
                GenreName = m.Genre?.Name ?? ""
            };
        }

        public async Task<MovieDto> CreateAsync(CreateMovieRequest request, string webRootPath)
        {
            string? imageUrl = null;

            if (request.ImageFile != null && request.ImageFile.Length > 0)
            {
                var uploads = Path.Combine(webRootPath, "uploads");
                Directory.CreateDirectory(uploads);
                var fileName = $"{Guid.NewGuid()}_{request.ImageFile.FileName}";
                var filePath = Path.Combine(uploads, fileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                await request.ImageFile.CopyToAsync(stream);
                imageUrl = $"/uploads/{fileName}";
            }

            var movie = new Movie
            {
                Title = request.Title,
                Description = request.Description,
                Year = request.Year,
                Director = request.Director,
                GenreId = request.GenreId,
                ImageUrl = imageUrl ?? ""
            };

            await _movieRepository.AddAsync(movie);

            return new MovieDto
            {
                MovieId = movie.MovieId,
                Title = movie.Title,
                Description = movie.Description,
                Year = movie.Year,
                Director = movie.Director,
                GenreId = movie.GenreId,
                ImageUrl = movie.ImageUrl
            };
        }

        public async Task<MovieDto?> UpdateAsync(UpdateMovieRequest request, string webRootPath)
        {
            var movie = await _movieRepository.GetByIdAsync(request.MovieId);
            if (movie == null) return null;

            movie.Title = request.Title;
            movie.Description = request.Description;
            movie.Year = request.Year;
            movie.Director = request.Director;
            movie.GenreId = request.GenreId;

            if (request.ImageFile != null && request.ImageFile.Length > 0)
            {
                if (!string.IsNullOrEmpty(movie.ImageUrl))
                {
                    var oldPath = Path.Combine(webRootPath, movie.ImageUrl.TrimStart('/'));
                    if (File.Exists(oldPath))
                    {
                        File.Delete(oldPath);
                    }
                }

                var uploads = Path.Combine(webRootPath, "uploads");
                Directory.CreateDirectory(uploads);
                var fileName = $"{Guid.NewGuid()}_{request.ImageFile.FileName}";
                var filePath = Path.Combine(uploads, fileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                await request.ImageFile.CopyToAsync(stream);
                movie.ImageUrl = $"/uploads/{fileName}";
            }

            await _movieRepository.UpdateAsync(movie);

            return new MovieDto
            {
                MovieId = movie.MovieId,
                Title = movie.Title,
                Description = movie.Description,
                Year = movie.Year,
                Director = movie.Director,
                GenreId = movie.GenreId,
                ImageUrl = movie.ImageUrl
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _movieRepository.DeleteAsync(id);
        }

        public Task<MovieDto> CreateAsync(CreateMovieRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<MovieDto?> UpdateAsync(UpdateMovieRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
