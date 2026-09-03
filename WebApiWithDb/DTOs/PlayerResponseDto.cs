using System.ComponentModel.DataAnnotations;

namespace WebApiWithDb.DTOs;

public class PlayerResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<GameSummaryDto> Games { get; set; } = new();
}