using Foosball.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foosball.Infrastructure.Entities
{
    [Table("Goals")]
    public class GoalEntity
    {
        public Guid Id { get; set; }
        public Guid MatchId { get; set; }
        public Guid ScoringPlayerId { get; set; }
        public bool IsOwnGoal { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public MatchEntity Match { get; set; } = null!;

        public static GoalEntity FromDomain(Guid matchId, FoosballGoal goal)
        {
            return new GoalEntity
            {
                MatchId = matchId,
                ScoringPlayerId = goal.PlayerId,
                IsOwnGoal = goal.IsOwnGoal,
                Id = goal.Id,
                Timestamp = goal.Timestamp
            };
        }
    }
}
