namespace Domain
{
    public class Team
    {
        public List<Player> Players { get; private set; }

        public Team(IEnumerable<Player> players)
        {
            if (players == null) throw new ArgumentNullException(nameof(players));
            var playerList = players.ToList();
            if (playerList.Count != 1 && playerList.Count != 2)
                throw new ArgumentException("A team must have 1 or 2 players.");
            Players = playerList;
        }
    }
}
