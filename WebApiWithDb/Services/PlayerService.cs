using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiWithDb.Data;
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
            existingPlayer.Score = player.Score;

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

        public async Task<bool> UpdateScoreAsync(int id, int newScore)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return false;
            }

            player.Score = newScore;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Player>> GetLeaderboardAsync(int topCount = 10)
        {
            return await _context.Players
                .Include(p => p.Games)
                .AsNoTracking()
                .OrderByDescending(p => p.Score)
                .Take(topCount)
                .ToListAsync();
        }
    }
}