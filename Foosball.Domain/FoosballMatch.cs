using Foosball.Domain;

namespace Domain
{
    public class FoosballMatch
    {
        public Guid Id { get; private set; }
        public Team TeamA { get; private set; }
        public Team TeamB { get; private set; }
        public List<FoosballGoal> ScoreA { get; private set; }
        public List<FoosballGoal> ScoreB { get; private set; }
        public bool IsFinished { get; private set; }

        private FoosballMatch(Team teamA, Team teamB)
        {
            Id = Guid.NewGuid();
            TeamA = teamA;
            TeamB = teamB;
            ScoreA = [];
            ScoreB = [];
            IsFinished = false;
        }

        public void GoalForTeamA(FoosballGoal goal)
        {
            if (IsFinished) throw new InvalidOperationException("Match is finished.");
            ScoreA.Add(goal);
        }

        public void GoalForTeamB(FoosballGoal goal)
        {
            if (IsFinished) throw new InvalidOperationException("Match is finished.");
            ScoreB.Add(goal);
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

        public static FoosballMatch Create(Team teamA, Team teamB)
        {
            if (teamA == null || teamB == null)
                throw new ArgumentNullException("Teams cannot be null.");

            if (!IsValidTeamSize(teamA, teamB))
                throw new ArgumentException("Both teams must have the same number of players (1 or 2).");

            return new FoosballMatch(teamA, teamB);
        }

        public static FoosballMatch FromExisting(Guid id, Team teamA, Team teamB, List<FoosballGoal> scoreA, List<FoosballGoal> scoreB, bool isFinished = false)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id cannot be empty.", nameof(id));
            if (teamA == null || teamB == null)
                throw new ArgumentNullException("Teams cannot be null.");
            var match = new FoosballMatch(teamA, teamB)
            {
                Id = id,
                ScoreA = scoreA ?? [],
                ScoreB = scoreB ?? []
            };
            return match;
        }
    }
}
