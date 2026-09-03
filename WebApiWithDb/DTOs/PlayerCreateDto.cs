using System.ComponentModel.DataAnnotations;

namespace WebApiWithDb.DTOs;

public class PlayerCreateDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}
