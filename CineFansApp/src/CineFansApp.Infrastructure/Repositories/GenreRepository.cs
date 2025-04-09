using CineFansApp.Domain.Entities;
using CineFansApp.Infrastructure.Data;

namespace CineFansApp.Infrastructure.Repositories
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(AppDbContext context) : base(context)
        {
        }
    }
}
