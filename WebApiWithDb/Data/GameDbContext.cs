using Microsoft.EntityFrameworkCore;
using WebApiWithDb.Models;

namespace WebApiWithDb.Data
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { }

        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Player>()
                .HasMany(p => p.Games)
                .WithMany(g => g.Players);
        }
    }
}