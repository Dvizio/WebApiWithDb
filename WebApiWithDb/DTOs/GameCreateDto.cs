using System.ComponentModel.DataAnnotations;
namespace WebApiWithDb.DTOs;

public class GameCreateDto
{
    [Required]
    [MaxLength(100)]
    public string GameId { get; set; } = string.Empty;
}