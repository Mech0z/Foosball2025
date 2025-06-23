
namespace Foosball.Domain
{
    public class Team
    {
        public Player Defender { get; private set; }
        public Player Attacker { get; private set; }

        public Team(Player singlePlayer)
        {
            if (singlePlayer == null) throw new ArgumentNullException(nameof(singlePlayer));
            
            Defender = singlePlayer;
            Attacker = singlePlayer; // In a single-player team, both roles are the same player
        }

        public Team(Player defender, Player attacker)
        {
            if (defender == null) throw new ArgumentNullException(nameof(defender));
            if (attacker == null) throw new ArgumentNullException(nameof(attacker));
            
            Defender = defender;
            Attacker = attacker;
        }

        public int PlayerCount
        {
            get => Defender == Attacker ? 1 : 2;
        }
    }
}
