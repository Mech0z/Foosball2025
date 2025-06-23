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

        public FoosballMatch(Team teamA, Team teamB)
        {
            if (teamA == null || teamB == null)
                throw new ArgumentNullException("Teams cannot be null.");

            if (!IsValidTeamSize(teamA, teamB))
                throw new ArgumentException("Both teams must have the same number of players (1 or 2).");

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

        private bool IsValidTeamSize(Team teamA, Team teamB)
        {
            int size = teamA.PlayerCount;
            return (size == 1 || size == 2) && teamB.PlayerCount == size;
        }
    }
}
