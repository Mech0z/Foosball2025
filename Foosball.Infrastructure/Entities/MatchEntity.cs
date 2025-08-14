using System.ComponentModel.DataAnnotations.Schema;
using Foosball.Domain;
using Domain;

namespace Foosball.Infrastructure.Entities
{
    [Table("Matches")]
    public class MatchEntity
    {
        public Guid Id { get; set; }
        public List<GoalEntity> Goals { get; set; } = new List<GoalEntity>();
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? FinishedAt { get; set; }

        public Guid Team1DefenderId { get; set; }
        [ForeignKey(nameof(Team1DefenderId))]
        public PlayerEntity Team1Defender { get; set; } = null!;

        public Guid Team1AttackerId { get; set; }
        [ForeignKey(nameof(Team1AttackerId))]
        public PlayerEntity Team1Attacker { get; set; } = null!;

        public Guid Team2DefenderId { get; set; }
        [ForeignKey(nameof(Team2DefenderId))]
        public PlayerEntity Team2Defender { get; set; } = null!;

        public Guid Team2AttackerId { get; set; }
        [ForeignKey(nameof(Team2AttackerId))]
        public PlayerEntity Team2Attacker { get; set; } = null!;

        public FoosballMatch ToDomain(List<Player> players)
        {
            Team teamA = new Team(
                players.Single(p => p.Id == Team1DefenderId),
                players.Single(p => p.Id == Team1AttackerId)
            );
            Team teamB = new Team(
                players.Single(p => p.Id == Team2DefenderId),
                players.Single(p => p.Id == Team2AttackerId)
            );
            var goals = Goals
                .Select(g => FoosballGoal.FromExisting(g.Id, g.ScoringPlayerId, g.IsOwnGoal, g.Timestamp))
                .ToList();

            return FoosballMatch.FromExisting(
                Id,
                teamA,
                teamB,
                goals,
                FinishedAt.HasValue
            );
        }

        public static MatchEntity FromDomain(FoosballMatch match)
        {
            return new MatchEntity
            {
                Id = match.Id,
                Team1DefenderId = match.TeamA.Defender.Id,
                Team1AttackerId = match.TeamA.Attacker.Id,
                Team2DefenderId = match.TeamB.Defender.Id,
                Team2AttackerId = match.TeamB.Attacker.Id,
                CreatedAt = DateTimeOffset.UtcNow,
                FinishedAt = match.IsFinished ? DateTimeOffset.UtcNow : null,
                Goals = match.Goals
                    .Select(goal => GoalEntity.FromDomain(match.Id, goal))
                    .ToList()
            };
        }
    }
}
