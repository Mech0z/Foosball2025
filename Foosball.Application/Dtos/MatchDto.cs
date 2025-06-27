namespace Foosball.Application.Dtos
{
    public record MatchDto(Guid MatchId, TeamDto Team1, TeamDto Team2, int ScoreTeam1, int ScoreTeam2)
    {
        public MatchDto(TeamDto team1, TeamDto team2, int scoreTeam1, int scoreTeam2) 
            : this(Guid.NewGuid(), team1 ?? throw new ArgumentNullException(nameof(team1)), team2 ?? throw new ArgumentNullException(nameof(team2)), scoreTeam1, scoreTeam2)
        {
        }
    }
}