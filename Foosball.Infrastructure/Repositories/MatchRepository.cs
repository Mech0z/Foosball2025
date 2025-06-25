using Foosball.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Foosball.Infrastructure.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly FoosballDbContext _dbContext;

        public MatchRepository(FoosballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<MatchEntity>> GetMatches()
        {
            var matches = await _dbContext.Matches.AsNoTracking().ToListAsync();
            return matches;
        }

        public Task<MatchEntity> GetMatchById(Guid matchId)
        {
            var match = _dbContext.Matches.AsNoTracking().FirstOrDefaultAsync(m => m.Id == matchId);
            if (match == null)
            {
                throw new KeyNotFoundException($"Match with ID {matchId} not found.");
            }
            return match;
        }

        public async Task<Guid> SaveMatch(MatchEntity match)
        {           
            _dbContext.Matches.Add(match);
            var matchId = await _dbContext.SaveChangesAsync().ContinueWith(t => match.Id);
            return matchId;
        }
    }
}
