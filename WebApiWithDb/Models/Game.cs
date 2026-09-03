using System.ComponentModel.DataAnnotations;

namespace WebApiWithDb.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string GameId { get; set; } = string.Empty;
        public string Winner { get; set; } = string.Empty;
        public bool GameFinished { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<Player> Players { get; set; } = new List<Player>();
    }
}