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
    }
}
