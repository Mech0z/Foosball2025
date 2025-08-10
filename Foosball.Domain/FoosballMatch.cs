using Foosball.Domain;

namespace Domain
{
    public class FoosballMatch
    {
        public Guid Id { get; private set; }
        public Team TeamA { get; private set; }
        public Team TeamB { get; private set; }
        public List<FoosballGoal> Goals { get; private set; }
        public bool IsFinished { get; private set; }

        private FoosballMatch(Team teamA, Team teamB)
        {
            Id = Guid.NewGuid();
            TeamA = teamA;
            TeamB = teamB;
            Goals = [];
            IsFinished = false;
        }

        public void RecordGoal(FoosballGoal goal)
        {
            if (IsFinished) throw new InvalidOperationException("Match is finished.");
            Goals.Add(goal);
        }

        public void FinishMatch()
        {
            IsFinished = true;
        }

        public bool IsValid()
        {
            return true;
        }

        private static bool IsValidTeamSize(Team teamA, Team teamB)
        {
            int size = teamA.PlayerCount;
            return (size == 1 || size == 2) && teamB.PlayerCount == size;
        }

        public int GetTeamAScore()
        {
            var scoredGoals = Goals.Count(goal => goal.PlayerId == TeamA.Defender.Id || goal.PlayerId == TeamA.Attacker.Id);
            var ownGoals = Goals.Count(goal => goal.IsOwnGoal && (goal.PlayerId == TeamB.Defender.Id || goal.PlayerId == TeamB.Attacker.Id));
            return scoredGoals + ownGoals;
        }

        public int GetTeamBScore()
        {
            var scoredGoals = Goals.Count(goal => goal.PlayerId == TeamB.Defender.Id || goal.PlayerId == TeamB.Attacker.Id);
            var ownGoals = Goals.Count(goal => goal.IsOwnGoal && (goal.PlayerId == TeamA.Defender.Id || goal.PlayerId == TeamA.Attacker.Id));
            return scoredGoals + ownGoals;
        }

        public static FoosballMatch Create(Team teamA, Team teamB)
        {
            if (teamA == null || teamB == null)
                throw new ArgumentNullException("Teams cannot be null.");

            if (!IsValidTeamSize(teamA, teamB))
                throw new ArgumentException("Both teams must have the same number of players (1 or 2).");

            return new FoosballMatch(teamA, teamB);
        }

        public static FoosballMatch FromExisting(Guid id, Team teamA, Team teamB, List<FoosballGoal> goals, bool isFinished = false)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id cannot be empty.", nameof(id));
            if (teamA == null || teamB == null)
                throw new ArgumentNullException("Teams cannot be null.");
            var match = new FoosballMatch(teamA, teamB)
            {
                Id = id,
                Goals = goals ?? new List<FoosballGoal>(),
                IsFinished = isFinished
            };
            return match;
        }
    }
}
