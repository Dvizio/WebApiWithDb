using System.ComponentModel.DataAnnotations;

namespace WebApiWithDb.DTOs
{
    public class PlayerCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public int Score { get; set; } = 0;
    }

    public class PlayerUpdateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public int Score { get; set; }
    }

    public class PlayerScoreUpdateDto
    {
        [Required]
        public int Score { get; set; }
    }

    public class PlayerResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Score { get; set; }
        public List<GameSummaryDto> Games { get; set; } = new();
    }

    public class PlayerSummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Score { get; set; }
    }
}

