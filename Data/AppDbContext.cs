using Microsoft.EntityFrameworkCore;
using AvyaktSandesh.Models;

namespace AvyaktSandesh.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Titles> Titles { get; set; }
        public DbSet<Articles> Articles { get; set; }
        public DbSet<MediaFiles> MediaFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Titles - Articles (one-to-many)
            modelBuilder.Entity<Titles>()
                .HasMany(t => t.Articles)
                .WithOne(a => a.Title)
                .HasForeignKey(a => a.TitleId)
                .OnDelete(DeleteBehavior.Cascade);

            // Articles - MediaFiles (one-to-many)
            modelBuilder.Entity<Articles>()
                .HasMany(a => a.MediaFiles)
                .WithOne(m => m.Article)
                .HasForeignKey(m => m.ArticleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}