using System.ComponentModel.DataAnnotations;

namespace WebApiWithDb.DTOs
{
    public class GameCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string GameId { get; set; } = string.Empty;
    }

    public class GameFinishDto
    {
        [Required]
        [MaxLength(100)]
        public string Winner { get; set; } = string.Empty;
    }

    public class AddPlayerToGameDto
    {
        [Required]
        public int PlayerId { get; set; }
    }

    public class GameScoreUpdateDto
    {
        [Required]
        public int PlayerId { get; set; }

        [Required]
        public int Score { get; set; }
    }

    public class GameScoreBoardUpdateDto
    {
        [Required]
        public Dictionary<int, int> ScoreBoard { get; set; } = new();
    }

    public class GameResponseDto
    {
        public int Id { get; set; }
        public string GameId { get; set; } = string.Empty;
        public string Winner { get; set; } = string.Empty;
        public bool GameFinished { get; set; }
        public DateTime CreatedAt { get; set; }
        public Dictionary<int, int> ScoreBoard { get; set; } = new();
        public List<PlayerSummaryDto> Players { get; set; } = new();
    }

    public class GameSummaryDto
    {
        public int Id { get; set; }
        public string GameId { get; set; } = string.Empty;
        public string Winner { get; set; } = string.Empty;
        public bool GameFinished { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
