using Microsoft.EntityFrameworkCore;
using CineFans.Domain.Entities;

public class CineFansDbContext : DbContext
{
    public CineFansDbContext(DbContextOptions<CineFansDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Follower> Followers { get; set; }
    public DbSet<Like> Likes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuración de Movies
        modelBuilder.Entity<Movie>()
            .HasOne(m => m.Genre)
            .WithMany(g => g.Movies)
            .HasForeignKey(m => m.GenreId)
            .OnDelete(DeleteBehavior.Restrict); // Coincide con ON DELETE NO ACTION

        // Configuración de Posts
        modelBuilder.Entity<Post>()
            .HasOne(p => p.User)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade); // ON DELETE CASCADE

        modelBuilder.Entity<Post>()
            .HasOne(p => p.Movie)
            .WithMany(m => m.Posts)
            .HasForeignKey(p => p.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configuración de Comments
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Post)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.NoAction); // ON DELETE NO ACTION

        // Configuración de Likes (tabla de unión)
        modelBuilder.Entity<Like>()
            .HasKey(l => new { l.PostId, l.UserId });

        modelBuilder.Entity<Like>()
            .HasOne(l => l.Post)
            .WithMany(p => p.Likes)
            .HasForeignKey(l => l.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Like>()
            .HasOne(l => l.User)
            .WithMany(u => u.Likes)
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        // Configuración de Followers (relación reflexiva)
        modelBuilder.Entity<Follower>()
            .HasKey(f => new { f.FollowerId, f.FollowedId });

        modelBuilder.Entity<Follower>()
            .HasOne(f => f.FollowerUser)
            .WithMany(u => u.Following)
            .HasForeignKey(f => f.FollowerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Follower>()
            .HasOne(f => f.FollowedUser)
            .WithMany(u => u.Followers)
            .HasForeignKey(f => f.FollowedId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}