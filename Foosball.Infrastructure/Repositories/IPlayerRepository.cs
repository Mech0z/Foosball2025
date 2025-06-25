using Foosball.Infrastructure.Entities;

namespace Foosball.Infrastructure.Repositories
{
    public interface IPlayerRepository
    {
        Task<List<PlayerEntity>> GetPlayers();
    }
}
