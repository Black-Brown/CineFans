using Microsoft.EntityFrameworkCore;
using CineFans.Domain.Entities;

namespace CineFans.Infrastructure.Context
{
    public class CineFansDbContext : DbContext
    {
        public CineFansDbContext(DbContextOptions<CineFansDbContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración ApplicationUse

            // Configuración extra de Movie
            modelBuilder.Entity<Movie>()
                .Property(m => m.GenreName)
                .HasMaxLength(50)
                .IsRequired();

            // Configuración extra de Comment
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Movie)
                .WithMany(m => m.Comments)
                .HasForeignKey(c => c.MovieId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
