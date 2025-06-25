namespace Foosball.Infrastructure.Entities
{
    public class GoalEntity
    {
        public Guid Id { get; set; }
        public Guid MatchId { get; set; }
        public Guid ScoringPlayerId { get; set; }
        public bool IsOwnGoal { get; set; }
        public DateTimeOffset ScoredAt { get; set; }
    }
}
