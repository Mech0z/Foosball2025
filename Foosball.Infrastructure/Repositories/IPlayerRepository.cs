using Foosball.Infrastructure.Entities;

namespace Foosball.Infrastructure.Repositories
{
    public interface IPlayerRepository
    {
        Task AddPlayer(PlayerEntity player);
        Task<List<PlayerEntity>> GetPlayers();
    }
}
