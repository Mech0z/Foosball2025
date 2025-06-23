namespace Domain
{
    public class FoosballMatch
    {
        public Guid Id { get; private set; }
        public Team TeamA { get; private set; }
        public Team TeamB { get; private set; }
        public List<Guid> ScoreA { get; private set; }
        public List<Guid> ScoreB { get; private set; }
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

        public void GoalForTeamA(Guid playerId)
        {
            if (IsFinished) throw new InvalidOperationException("Match is finished.");
            ScoreA.Add(playerId);
        }

        public void GoalForTeamB(Guid playerId)
        {
            if (IsFinished) throw new InvalidOperationException("Match is finished.");
            ScoreB.Add(playerId);
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
