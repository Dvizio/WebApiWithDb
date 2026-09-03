using System.ComponentModel.DataAnnotations;

namespace WebApiWithDb.DTOs;

public class GameScoreBoardUpdateDto
{
    [Required]
    public Dictionary<int, int> ScoreBoard { get; set; } = new();
}