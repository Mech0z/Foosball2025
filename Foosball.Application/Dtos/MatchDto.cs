namespace Foosball.Application.Dtos
{
    public record MatchDto(Guid MatchId, TeamDto Team1, TeamDto Team2, int ScoreTeam1, int ScoreTeam2, List<GoalDto> goals)
    {
    }
}