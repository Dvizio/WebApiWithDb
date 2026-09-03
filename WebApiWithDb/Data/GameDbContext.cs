using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

            // Configure Many-to-Many between Player and Game
            modelBuilder.Entity<Player>()
                .HasMany(p => p.Games)
                .WithMany(g => g.Players);

            // Configure ScoreBoard dictionary JSON conversion and value comparison
            var scoreBoardComparer = new ValueComparer<Dictionary<int, int>>(
                (c1, c2) => (c1 == null && c2 == null) || (c1 != null && c2 != null && c1.OrderBy(kv => kv.Key).SequenceEqual(c2.OrderBy(kv => kv.Key))),
                c => c == null ? 0 : c.Aggregate(0, (a, v) => HashCode.Combine(a, v.Key.GetHashCode(), v.Value.GetHashCode())),
                c => c == null ? new Dictionary<int, int>() : new Dictionary<int, int>(c)
            );

            modelBuilder.Entity<Game>()
                .Property(g => g.ScoreBoard)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null!),
                    v => JsonSerializer.Deserialize<Dictionary<int, int>>(v, (JsonSerializerOptions)null!) ?? new Dictionary<int, int>()
                )
                .Metadata
                .SetValueComparer(scoreBoardComparer);
        }
    }
}