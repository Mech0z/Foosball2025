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
        public IActionResult StartMatch([FromBody] StartMatchRequest request)
        {
            var matchId = matchService.StartMatch(request);

            return Ok(new { matchId });
        }
        
        [HttpPost]
        public IActionResult GoalScored([FromBody] GoalScoredRequest request)
        {
            // Assuming there's a method in matchService to handle goal scoring
            // matchService.RecordGoal(request.MatchId, request.ScoringPlayerId);
            return Ok(new { message = "Goal recorded successfully." });
        }
    }
}
