// En CineFans.Infrastructure.Repositories
using CineFans.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class MovieRepository : IMovieRepository
{
    private readonly CineFansDbContext _context;

    public MovieRepository(CineFansDbContext context)
    {
        _context = context;
    }

    public async Task<Movie> GetByIdAsync(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null)
        {
            throw new KeyNotFoundException($"Movie with ID {id} not found.");
        }
        return movie;
    }

    public async Task<IEnumerable<Movie>> GetAllAsync()
    {
        return await _context.Movies.ToListAsync();
    }

    public async Task<Movie> CreateAsync(Movie movie)
    {
        await _context.Movies.AddAsync(movie);
        await _context.SaveChangesAsync();
        return movie;
    }

    public async Task<bool> UpdateAsync(Movie movie)
    {
        _context.Movies.Update(movie);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null) return false;

        _context.Movies.Remove(movie);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<Movie> AddAsync(Movie movie)
    {
        await _context.Movies.AddAsync(movie);
        await _context.SaveChangesAsync();
        return movie;
    }

    public async Task<string?> GetUserNameByIdAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        return user?.Name ?? "Unknown User";
    }
}
