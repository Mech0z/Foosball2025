using Foosball.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Foosball.Infrastructure.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly FoosballDbContext _dbContext;

        public PlayerRepository(FoosballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<PlayerEntity>> GetPlayers()
        {
            var players = await _dbContext.Players.AsNoTracking().ToListAsync();
            return players;
        }
    }
}
