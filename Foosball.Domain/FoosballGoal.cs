namespace Foosball.Domain
{
    public class FoosballGoal
    {
        public Guid Id { get; private set; }
        public Guid PlayerId { get; private set; }
        public DateTime Timestamp { get; private set; }
    }
}
