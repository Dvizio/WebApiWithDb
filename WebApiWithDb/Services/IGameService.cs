using WebApiWithDb.Models;

namespace WebApiWithDb.Services
{
    public interface IGameService
    {
        Task<IEnumerable<Game>> GetAllGamesAsync();
        Task<Game?> GetGameByIdAsync(int id);
        Task<Game?> GetGameByCustomIdAsync(string gameId);
        Task<Game> CreateGameAsync(string gameId);
        Task<bool> FinishGameAsync(int id, string winner);
        Task<bool> AddPlayerToGameAsync(int gameId, int playerId);
        Task<bool> UpdatePlayerScoreAsync(int gameId, int playerId, int score);
        Task<bool> UpdateScoreBoardAsync(int gameId, Dictionary<int, int> scoreBoard);
        Task<bool> DeleteGameAsync(int id);
    }
}