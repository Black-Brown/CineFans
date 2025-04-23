using CineFans.Domain.Entities;
using CineFans.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace CineFans.Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly CineFansDbContext _context;

        public MovieRepository(CineFansDbContext context)
        {
            _context = context;
        }

        public async Task<List<Movie>> GetAllAsync()
        {
            return await _context.Movies.Include(m => m.Genre).ToListAsync();
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            return await _context.Movies.Include(m => m.Genre)
                .FirstOrDefaultAsync(m => m.MovieId == id);
        }

        public async Task AddAsync(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Movie movie)
        {
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return false;

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
