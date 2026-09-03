using System.ComponentModel.DataAnnotations;

namespace WebApiWithDb.DTOs
{
    public class PlayerCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }

    public class PlayerUpdateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }

    public class PlayerResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<GameSummaryDto> Games { get; set; } = new();
    }

    public class PlayerSummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class PlayerLeaderboardDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int HighestScore { get; set; }
    }
}
