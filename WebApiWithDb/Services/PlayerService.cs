using Microsoft.EntityFrameworkCore;
using WebApiWithDb.Data;
using WebApiWithDb.DTOs;
using WebApiWithDb.Models;

namespace WebApiWithDb.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly GameDbContext _context;

        public PlayerService(GameDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Player>> GetAllPlayersAsync()
        {
            return await _context.Players
                .Include(p => p.Games)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Player?> GetPlayerByIdAsync(int id)
        {
            return await _context.Players
                .Include(p => p.Games)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Player> CreatePlayerAsync(Player player)
        {
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
            return player;
        }

        public async Task<bool> UpdatePlayerAsync(Player player)
        {
            var existingPlayer = await _context.Players.FindAsync(player.Id);
            if (existingPlayer == null)
            {
                return false;
            }

            existingPlayer.Name = player.Name;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePlayerAsync(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return false;
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PlayerLeaderboardDto>> GetLeaderboardAsync(int topCount = 10)
        {
            var players = await _context.Players
                .Include(p => p.Games)
                .AsNoTracking()
                .ToListAsync();

            var leaderboard = players.Select(p =>
            {
                var highestScore = 0;
                if (p.Games != null)
                {
                    foreach (var game in p.Games)
                    {
                        if (game.ScoreBoard != null && game.ScoreBoard.TryGetValue(p.Id, out var score))
                        {
                            if (score > highestScore)
                            {
                                highestScore = score;
                            }
                        }
                    }
                }

                return new PlayerLeaderboardDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    HighestScore = highestScore
                };
            })
            .OrderByDescending(p => p.HighestScore)
            .Take(topCount)
            .ToList();

            return leaderboard;
        }
    }
}