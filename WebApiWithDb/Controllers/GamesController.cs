using Microsoft.AspNetCore.Mvc;
using WebApiWithDb.DTOs;
using WebApiWithDb.Models;
using WebApiWithDb.Services;

namespace WebApiWithDb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // GET: api/games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameResponseDto>>> GetAllGames()
        {
            var games = await _gameService.GetAllGamesAsync();
            var response = games.Select(MapToResponseDto);
            return Ok(response);
        }

        // GET: api/games/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GameResponseDto>> GetGameById(int id)
        {
            var game = await _gameService.GetGameByIdAsync(id);
            if (game == null)
            {
                return NotFound(new { message = $"Game with ID {id} not found." });
            }

            return Ok(MapToResponseDto(game));
        }

        // GET: api/games/custom/{gameId}
        [HttpGet("custom/{gameId}")]
        public async Task<ActionResult<GameResponseDto>> GetGameByCustomId(string gameId)
        {
            var game = await _gameService.GetGameByCustomIdAsync(gameId);
            if (game == null)
            {
                return NotFound(new { message = $"Game with custom GameId '{gameId}' not found." });
            }

            return Ok(MapToResponseDto(game));
        }

        // POST: api/games
        [HttpPost]
        public async Task<ActionResult<GameResponseDto>> CreateGame([FromBody] GameCreateDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdGame = await _gameService.CreateGameAsync(createDto.GameId);
            var response = MapToResponseDto(createdGame);

            return CreatedAtAction(nameof(GetGameById), new { id = createdGame.Id }, response);
        }

        // POST: api/games/{id}/players
        [HttpPost("{id:int}/players")]
        public async Task<IActionResult> AddPlayerToGame(int id, [FromBody] AddPlayerToGameDto addPlayerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _gameService.AddPlayerToGameAsync(id, addPlayerDto.PlayerId);
            if (!success)
            {
                return NotFound(new { message = $"Unable to add player {addPlayerDto.PlayerId} to game {id}. Please verify both IDs exist." });
            }

            return NoContent();
        }

        // POST: api/games/{id}/finish
        [HttpPost("{id:int}/finish")]
        public async Task<IActionResult> FinishGame(int id, [FromBody] GameFinishDto finishDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _gameService.FinishGameAsync(id, finishDto.Winner);
            if (!success)
            {
                return NotFound(new { message = $"Game with ID {id} not found." });
            }

            return NoContent();
        }

        // DELETE: api/games/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var deleted = await _gameService.DeleteGameAsync(id);
            if (!deleted)
            {
                return NotFound(new { message = $"Game with ID {id} not found." });
            }

            return NoContent();
        }

        private static GameResponseDto MapToResponseDto(Game game)
        {
            return new GameResponseDto
            {
                Id = game.Id,
                GameId = game.GameId,
                Winner = game.Winner,
                GameFinished = game.GameFinished,
                CreatedAt = game.CreatedAt,
                Players = game.Players?.Select(p => new PlayerSummaryDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Score = p.Score
                }).ToList() ?? new List<PlayerSummaryDto>()
            };
        }
    }
}

