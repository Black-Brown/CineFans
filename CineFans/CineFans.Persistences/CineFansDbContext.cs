using Microsoft.EntityFrameworkCore;
using CineFans.Domain.Entities;

    public class CineFansDbContext : DbContext
    {
        public CineFansDbContext (DbContextOptions<CineFansDbContext> options)
            : base(options)
        {
        }

        public DbSet<CineFans.Domain.Entities.User> Users { get; set; } = default!;

        public DbSet<CineFans.Domain.Entities.Movie> Movies { get; set; } = default!;

        public DbSet<CineFans.Domain.Entities.Genre> Genres { get; set; } = default!;

        public DbSet<CineFans.Domain.Entities.Post> Posts { get; set; } = default!;

        public DbSet<CineFans.Domain.Entities.Comment> Comments { get; set; } = default!;

        public DbSet<CineFans.Domain.Entities.Follower> Followers { get; set; } = default!;

        public DbSet<CineFans.Domain.Entities.Like> Likes { get; set; } = default!;

    }
