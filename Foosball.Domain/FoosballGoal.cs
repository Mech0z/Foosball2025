namespace Foosball.Domain
{
    public class FoosballGoal
    {
        public Guid Id { get; private set; }
        public Guid PlayerId { get; private set; }
        public DateTimeOffset Timestamp { get; private set; }
        public bool IsOwnGoal { get; private set; }

        public static FoosballGoal Create(Guid playerId, bool isOwnGoal)
        {
            if (playerId == Guid.Empty)
                throw new ArgumentException("Player ID cannot be empty.", nameof(playerId));
            return new FoosballGoal
            {
                Id = Guid.NewGuid(),
                PlayerId = playerId,
                IsOwnGoal = isOwnGoal,
                Timestamp = DateTimeOffset.UtcNow
            };
        }

        public static FoosballGoal FromExisting(Guid id, Guid playerId, bool isOwnGoal, DateTimeOffset timestamp)
        {
            return new FoosballGoal
            {
                Id = id,
                PlayerId = playerId,
                IsOwnGoal = isOwnGoal,
                Timestamp = timestamp
            };
        }
    }
}
