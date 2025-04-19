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
    }
