using CineFansApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CineFansApp.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Follow> Follows { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure table names to match SQL schema
            modelBuilder.Entity<User>().ToTable("Usuarios");
            modelBuilder.Entity<Genre>().ToTable("Generos");
            modelBuilder.Entity<Movie>().ToTable("Peliculas");
            modelBuilder.Entity<Post>().ToTable("Publicaciones");
            modelBuilder.Entity<Comment>().ToTable("Comentarios");
            modelBuilder.Entity<Like>().ToTable("Likes");
            modelBuilder.Entity<Follow>().ToTable("Seguidores");

            // Configure primary keys
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<Genre>().HasKey(g => g.GeneroId);
            modelBuilder.Entity<Movie>().HasKey(m => m.PeliculaId);
            modelBuilder.Entity<Post>().HasKey(p => p.PublicacionId);
            modelBuilder.Entity<Comment>().HasKey(c => c.ComentarioId);
            modelBuilder.Entity<Like>().HasKey(l => new { l.PublicacionId, l.UsuarioId });
            modelBuilder.Entity<Follow>().HasKey(f => new { f.SeguidorId, f.SeguidoId });

            // Configure relationships
            modelBuilder.Entity<Movie>()
                .HasOne(m => m.Genre)
                .WithMany(g => g.Movies)
                .HasForeignKey(m => m.GeneroId);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UsuarioId);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Movie)
                .WithMany(m => m.Posts)
                .HasForeignKey(p => p.PeliculaId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PublicacionId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UsuarioId);

            modelBuilder.Entity<Like>()
                .HasOne(l => l.Post)
                .WithMany(p => p.Likes)
                .HasForeignKey(l => l.PublicacionId);

            modelBuilder.Entity<Like>()
                .HasOne(l => l.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.UsuarioId);

            // Configure Follow relationship (many-to-many self-referencing)
            modelBuilder.Entity<Follow>()
                .HasOne(f => f.Follower)
                .WithMany(u => u.Following)
                .HasForeignKey(f => f.SeguidorId);

            modelBuilder.Entity<Follow>()
                .HasOne(f => f.Following)
                .WithMany(u => u.Followers)
                .HasForeignKey(f => f.SeguidoId);
        }
    }
}
