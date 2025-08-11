using System.ComponentModel.DataAnnotations.Schema;

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
    }
}
