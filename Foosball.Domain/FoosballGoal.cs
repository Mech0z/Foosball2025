namespace Foosball.Domain
{
    public class FoosballGoal
    {
        public Guid Id { get; private set; }
        public Guid PlayerId { get; private set; }
        public DateTimeOffset Timestamp { get; private set; }
        public bool IsOwnGoal { get; private set; }

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
