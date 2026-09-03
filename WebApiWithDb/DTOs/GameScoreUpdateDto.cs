using System.ComponentModel.DataAnnotations;

namespace WebApiWithDb.DTOs;

public class GameScoreUpdateDto
{
    [Required]
    public int PlayerId { get; set; }

    [Required]
    public int Score { get; set; }
}