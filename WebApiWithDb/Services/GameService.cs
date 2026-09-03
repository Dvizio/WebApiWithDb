using Microsoft.EntityFrameworkCore;
using WebApiWithDb.Data;
using WebApiWithDb.Models;

namespace WebApiWithDb.Services
{
    public class GameService : IGameService
    {
        private readonly GameDbContext _context;

        public GameService(GameDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Game>> GetAllGamesAsync()
        {
            return await _context.Games
                .Include(g => g.Players)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Game?> GetGameByIdAsync(int id)
        {
            return await _context.Games
                .Include(g => g.Players)
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<Game?> GetGameByCustomIdAsync(string gameId)
        {
            return await _context.Games
                .Include(g => g.Players)
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.GameId == gameId);
        }

        public async Task<Game> CreateGameAsync(string gameId)
        {
            var game = new Game
            {
                GameId = gameId,
                CreatedAt = DateTime.UtcNow,
                GameFinished = false,
                Winner = string.Empty,
                ScoreBoard = new Dictionary<int, int>()
            };

            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            return game;
        }

        public async Task<bool> FinishGameAsync(int id, string winner)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null) return false;

            game.GameFinished = true;
            game.Winner = winner;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddPlayerToGameAsync(int gameId, int playerId)
        {
            var game = await _context.Games
                .Include(g => g.Players)
                .FirstOrDefaultAsync(g => g.Id == gameId);

            var player = await _context.Players.FindAsync(playerId);

            if (game == null || player == null) return false;

            if (!game.Players.Any(p => p.Id == playerId))
            {
                game.Players.Add(player);
            }

            game.ScoreBoard ??= new Dictionary<int, int>();
            if (!game.ScoreBoard.ContainsKey(playerId))
            {
                game.ScoreBoard[playerId] = 0;
            }

            _context.Entry(game).Property(g => g.ScoreBoard).IsModified = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdatePlayerScoreAsync(int gameId, int playerId, int score)
        {
            var game = await _context.Games
                .Include(g => g.Players)
                .FirstOrDefaultAsync(g => g.Id == gameId);

            if (game == null) return false;

            var playerExists = await _context.Players.AnyAsync(p => p.Id == playerId);
            if (!playerExists) return false;

            // Ensure player is part of game if not already
            if (!game.Players.Any(p => p.Id == playerId))
            {
                var player = await _context.Players.FindAsync(playerId);
                if (player != null)
                {
                    game.Players.Add(player);
                }
            }

            var updatedScoreBoard = new Dictionary<int, int>(game.ScoreBoard ?? new Dictionary<int, int>())
            {
                [playerId] = score
            };
            game.ScoreBoard = updatedScoreBoard;

            _context.Entry(game).Property(g => g.ScoreBoard).IsModified = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateScoreBoardAsync(int gameId, Dictionary<int, int> scoreBoard)
        {
            var game = await _context.Games
                .Include(g => g.Players)
                .FirstOrDefaultAsync(g => g.Id == gameId);

            if (game == null) return false;

            // Ensure players in scoreboard are added to game
            foreach (var playerId in scoreBoard.Keys)
            {
                if (!game.Players.Any(p => p.Id == playerId))
                {
                    var player = await _context.Players.FindAsync(playerId);
                    if (player != null)
                    {
                        game.Players.Add(player);
                    }
                }
            }

            game.ScoreBoard = new Dictionary<int, int>(scoreBoard);
            _context.Entry(game).Property(g => g.ScoreBoard).IsModified = true;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteGameAsync(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null) return false;

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}