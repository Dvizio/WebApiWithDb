using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiWithDb.Data; 
using WebApiWithDb.Models;

namespace WebApiWithDb.Services;
 public interface IPlayerService
    {
        Task<IEnumerable<Player>> GetAllPlayersAsync();
        Task<Player?> GetPlayerByIdAsync(int id);
        Task<Player> CreatePlayerAsync(Player player);
        Task<bool> UpdatePlayerAsync(Player player);
        Task<bool> DeletePlayerAsync(int id);
        Task<bool> UpdateScoreAsync(int id, int newScore);
        Task<IEnumerable<Player>> GetLeaderboardAsync(int topCount = 10);
    }