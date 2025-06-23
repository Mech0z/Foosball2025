using Foosball.Infrastructure.Entities;

namespace Foosball.Infrastructure.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        public PlayerRepository() { }

        public List<PlayerEntity> GetPlayers()
        {
            // This method should return a list of players.
            // For now, we return an empty list as a placeholder.
            return new List<PlayerEntity>();
        }
    }
}
