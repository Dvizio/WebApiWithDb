
using System.ComponentModel.DataAnnotations;
namespace WebApiWithDb.DTOs;

public class GameResponseDto
{
    public int Id { get; set; }
    public string GameId { get; set; } = string.Empty;
    public string Winner { get; set; } = string.Empty;
    public bool GameFinished { get; set; }
    public DateTime CreatedAt { get; set; }
    public Dictionary<int, int> ScoreBoard { get; set; } = new();
    public List<PlayerSummaryDto> Players { get; set; } = new();
}