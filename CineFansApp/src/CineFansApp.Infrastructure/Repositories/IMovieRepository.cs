using CineFansApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CineFansApp.Infrastructure.Repositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<List<Movie>> GetPopularMoviesAsync(int count);
    }
}
