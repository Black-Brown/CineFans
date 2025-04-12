using CineFansApp.Domain.Entities;
using CineFansApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineFansApp.Infrastructure.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<Movie?> GetByIdAsync(int id)
        {
            return await _context.Movies
                .Include(m => m.Genre)
                .FirstOrDefaultAsync(m => m.PeliculaId == id);
        }

        public override async Task<List<Movie>> GetAllAsync()
        {
            return await _context.Movies
                .Include(m => m.Genre)
                .ToListAsync();
        }

        public async Task<List<Movie>> GetPopularMoviesAsync(int count)
        {
            // Obtener películas con más publicaciones
            return await _context.Posts
                .GroupBy(p => p.PeliculaId)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .Take(count)
                .Join(
                    _context.Movies.Include(m => m.Genre),
                    postMovieId => postMovieId,
                    movie => movie.PeliculaId,
                    (postMovieId, movie) => movie
                )
                .ToListAsync();
        }
    }
}
