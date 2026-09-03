using WebApiWithDb.DTOs;
using WebApiWithDb.Models;

namespace WebApiWithDb.Services
{
    public interface IPlayerService
    {
        Task<IEnumerable<Player>> GetAllPlayersAsync();
        Task<Player?> GetPlayerByIdAsync(int id);
        Task<Player> CreatePlayerAsync(Player player);
        Task<bool> UpdatePlayerAsync(Player player);
        Task<bool> DeletePlayerAsync(int id);
        Task<IEnumerable<PlayerLeaderboardDto>> GetLeaderboardAsync(int topCount = 10);
    }
}