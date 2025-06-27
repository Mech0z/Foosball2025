namespace Foosball.Infrastructure.Entities
{
    public class MatchEntity
    {
        public Guid Id { get; set; }
        public List<GoalEntity> Goals { get; set; } = new List<GoalEntity>();
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? FinishedAt { get; set; }
        public Guid Team1DefenderId { get; set; }
        public Guid Team1AttackerId { get; set; }
        public Guid Team2DefenderId { get; set; }
        public Guid Team2AttackerId { get; set; }
    }
}
