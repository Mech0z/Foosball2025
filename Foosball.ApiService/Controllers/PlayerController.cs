using Foosball.Application.Dtos;
using Foosball.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Foosball.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService playerService;

        public PlayerController(IPlayerService playerService)
        {
            this.playerService = playerService;
        }

        [HttpPost("create-player")]
        public async Task<IActionResult> CreatePlayerAsync([FromBody] CreatePlayerRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest("Player name cannot be empty.");
            }
            var playerId = await playerService.CreatePlayerAsync(request.Name);
            return Ok(new { PlayerId = playerId });
        }

        [HttpGet("get-players")]
        public async Task<IActionResult> GetPlayersAsync()
        {
            var players = await playerService.GetPlayersAsync();
            return Ok(players);
        }
    }
}
