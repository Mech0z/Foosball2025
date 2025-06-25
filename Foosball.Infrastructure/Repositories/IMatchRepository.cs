using Foosball.Infrastructure.Entities;

namespace Foosball.Infrastructure.Repositories
{
    public interface IMatchRepository
    {
        Task<MatchEntity> GetMatchById(Guid matchId);
        Task<List<MatchEntity>> GetMatches();
        Task<Guid> SaveMatch(MatchEntity match);
    }
}