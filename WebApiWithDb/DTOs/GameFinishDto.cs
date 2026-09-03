using System.ComponentModel.DataAnnotations;

namespace WebApiWithDb.DTOs;

public class GameFinishDto

{
    [Required]
    [MaxLength(100)]
    public string Winner { get; set; } = string.Empty;
}