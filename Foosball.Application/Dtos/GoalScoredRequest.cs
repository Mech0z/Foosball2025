namespace Foosball.Application.Dtos
{
    public class GoalScoredRequest
    {
        public Guid MatchId { get; set; }
        public Guid ScoringPlayerId { get; set; }
        public bool IsOwnGoal { get; set; }
        public GoalScoredRequest(Guid matchId, Guid scoringPlayerId)
        {
            MatchId = matchId;
            ScoringPlayerId = scoringPlayerId;
        }
    }
}
