using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CineFans.Web.Models;

    public class CineFansDbContext : DbContext
    {
        public CineFansDbContext (DbContextOptions<CineFansDbContext> options)
            : base(options)
        {
        }

        public DbSet<CineFans.Web.Models.Users> Users { get; set; } = default!;

        public DbSet<CineFans.Web.Models.Movie> Movies { get; set; } = default!;

        public DbSet<CineFans.Web.Models.Genre> Genres { get; set; } = default!;

        public DbSet<CineFans.Web.Models.Posts> Posts { get; set; } = default!;

        public DbSet<CineFans.Web.Models.Comments> Comments { get; set; } = default!;

        public DbSet<CineFans.Web.Models.Followers> Followers { get; set; } = default!;

        public DbSet<CineFans.Web.Models.Likes> Likes { get; set; } = default!;

    }
