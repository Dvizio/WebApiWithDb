using System.ComponentModel.DataAnnotations;

namespace WebApiWithDb.DTOs;

public class AddPlayerToGameDto
{
    [Required]
    public int PlayerId { get; set; }
}