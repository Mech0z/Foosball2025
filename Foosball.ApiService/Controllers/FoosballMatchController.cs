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
        
        [HttpPost("record-goal")]
        public async Task<IActionResult> RecordGoalAsync([FromBody] GoalScoredRequest request)
        {
            await matchService.RecordGoal(request);
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
