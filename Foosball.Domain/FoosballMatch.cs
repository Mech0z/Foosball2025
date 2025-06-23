namespace Domain
{
    public class FoosballMatch
    {
        public Guid Id { get; private set; }
        public Team TeamA { get; private set; }
        public Team TeamB { get; private set; }
        public int ScoreA { get; private set; }
        public int ScoreB { get; private set; }
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
            ScoreA = 0;
            ScoreB = 0;
            IsFinished = false;
        }

        public void GoalForTeamA()
        {
            if (IsFinished) throw new InvalidOperationException("Match is finished.");
            ScoreA++;
        }

        public void GoalForTeamB()
        {
            if (IsFinished) throw new InvalidOperationException("Match is finished.");
            ScoreB++;
        }

        public void FinishMatch()
        {
            IsFinished = true;
        }

        private bool IsValidTeamSize(Team teamA, Team teamB)
        {
            int size = teamA.Players.Count;
            return (size == 1 || size == 2) && teamB.Players.Count == size;
        }
    }
}
