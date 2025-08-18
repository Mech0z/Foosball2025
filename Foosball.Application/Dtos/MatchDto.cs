using Domain;
using Foosball.Domain;

namespace Foosball.Application.Dtos
{
    public record MatchDto(Guid MatchId, TeamDto Team1, TeamDto Team2, int ScoreTeam1, int ScoreTeam2, List<GoalDto> goals)
    {
        public static MatchDto FromDomain(FoosballMatch match, List<Player> players)
        {
            PlayerDto MapPlayer(Player p) => new PlayerDto { Id = p.Id, Name = p.Name };

            return new MatchDto(
                match.Id,
                new TeamDto(
                    MapPlayer(match.TeamA.Defender),
                    MapPlayer(match.TeamA.Attacker)
                ),
                new TeamDto(
                    MapPlayer(match.TeamB.Defender),
                    MapPlayer(match.TeamB.Attacker)
                ),
                match.GetTeamAScore(),
                match.GetTeamBScore(),
                match.Goals.Select(goal => new GoalDto(
                    players.Single(x => x.Id == goal.PlayerId).Name,
                    goal.IsOwnGoal,
                    goal.Timestamp
                )).ToList()
            );
        }
    }
}