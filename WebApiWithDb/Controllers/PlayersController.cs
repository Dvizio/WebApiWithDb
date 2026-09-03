using Microsoft.AspNetCore.Mvc;
using WebApiWithDb.DTOs;
using WebApiWithDb.Models;
using WebApiWithDb.Services;

namespace WebApiWithDb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        // GET: api/players
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerResponseDto>>> GetAllPlayers()
        {
            var players = await _playerService.GetAllPlayersAsync();
            var response = players.Select(MapToResponseDto);
            return Ok(response);
        }

        // GET: api/players/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PlayerResponseDto>> GetPlayerById(int id)
        {
            var player = await _playerService.GetPlayerByIdAsync(id);
            if (player == null)
            {
                return NotFound(new { message = $"Player with ID {id} not found." });
            }

            return Ok(MapToResponseDto(player));
        }

        // GET: api/players/leaderboard?topCount=10
        [HttpGet("leaderboard")]
        public async Task<ActionResult<IEnumerable<PlayerLeaderboardDto>>> GetLeaderboard([FromQuery] int topCount = 10)
        {
            if (topCount <= 0)
            {
                topCount = 10;
            }

            var leaderboard = await _playerService.GetLeaderboardAsync(topCount);
            return Ok(leaderboard);
        }

        // POST: api/players
        [HttpPost]
        public async Task<ActionResult<PlayerResponseDto>> CreatePlayer([FromBody] PlayerCreateDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var player = new Player
            {
                Name = createDto.Name
            };

            var createdPlayer = await _playerService.CreatePlayerAsync(player);
            var response = MapToResponseDto(createdPlayer);

            return CreatedAtAction(nameof(GetPlayerById), new { id = createdPlayer.Id }, response);
        }

        // PUT: api/players/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePlayer(int id, [FromBody] PlayerUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var player = new Player
            {
                Id = id,
                Name = updateDto.Name
            };

            var updated = await _playerService.UpdatePlayerAsync(player);
            if (!updated)
            {
                return NotFound(new { message = $"Player with ID {id} not found." });
            }

            return NoContent();
        }

        // DELETE: api/players/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var deleted = await _playerService.DeletePlayerAsync(id);
            if (!deleted)
            {
                return NotFound(new { message = $"Player with ID {id} not found." });
            }

            return NoContent();
        }

        private static PlayerResponseDto MapToResponseDto(Player player)
        {
            return new PlayerResponseDto
            {
                Id = player.Id,
                Name = player.Name,
                Games = player.Games?.Select(g => new GameSummaryDto
                {
                    Id = g.Id,
                    GameId = g.GameId,
                    Winner = g.Winner,
                    GameFinished = g.GameFinished,
                    CreatedAt = g.CreatedAt
                }).ToList() ?? new List<GameSummaryDto>()
            };
        }
    }
}
