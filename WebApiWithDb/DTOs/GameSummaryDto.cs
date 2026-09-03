using System.ComponentModel.DataAnnotations;

namespace WebApiWithDb.DTOs;

public class GameSummaryDto
{
    public int Id { get; set; }
    public string GameId { get; set; } = string.Empty;
    public string Winner { get; set; } = string.Empty;
    public bool GameFinished { get; set; }
    public DateTime CreatedAt { get; set; }
}