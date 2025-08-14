using Domain;
using Foosball.Domain;

namespace Foosball.Application.Dtos
{
    public record MatchDto(Guid MatchId, TeamDto Team1, TeamDto Team2, int ScoreTeam1, int ScoreTeam2, List<GoalDto> goals)
    {
        public static MatchDto FromDomain(FoosballMatch match, List<Player> players)
        {
            return new MatchDto(
                match.Id,
                new TeamDto(match.TeamA.Defender.Name, match.TeamA.Attacker.Name),
                new TeamDto(match.TeamB.Defender.Name, match.TeamB.Attacker.Name),
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