using System.ComponentModel.DataAnnotations;

namespace WebApiWithDb.DTOs;

public class PlayerUpdateDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}