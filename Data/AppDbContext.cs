//using ArticleMediaApp.API.Models;
using AvyaktSandesh.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AvyaktSandesh.Data
{
    
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Titles> Titles { get; set; }
        public DbSet<Articles> Articles { get; set; }
        public DbSet<MediaFiles> MediaFiles { get; set; }
    }

}
