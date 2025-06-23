using Foosball.Infrastructure.Entities;

namespace Foosball.Infrastructure.Repositories
{
    public interface IPlayerRepository
    {
        List<PlayerEntity> GetPlayers();
    }
}
