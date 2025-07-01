using Foosball.Application.Dtos;
using Foosball.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Foosball.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoosballMatchController : ControllerBase
    {
        private readonly IMatchService matchService;

        public FoosballMatchController(IMatchService matchService)
        {
            this.matchService = matchService;
        }

        [HttpPost("start-match")]
        public async Task<IActionResult> StartMatchAsync([FromBody] StartMatchRequest request)
        {
            var matchId = await matchService.StartMatch(request);

            return Ok(new { matchId });
        }
        
        [HttpPost]
        public IActionResult GoalScored([FromBody] GoalScoredRequest request)
        {
            // Assuming there's a method in matchService to handle goal scoring

            // matchService.RecordGoal(request.MatchId, request.ScoringPlayerId);
            return Ok(new { message = "Goal recorded successfully." });
        }

        [HttpGet("get-matches")]
        public async Task<IActionResult> GetMatchesAsync()
        {
            var response = await matchService.GetMatches();
            return Ok(response);
        }
    }
}
