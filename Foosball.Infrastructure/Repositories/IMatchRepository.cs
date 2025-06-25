using Foosball.Infrastructure.Entities;

namespace Foosball.Infrastructure.Repositories
{
    public interface IMatchRepository
    {
        MatchEntity GetMatchById(Guid matchId);
        List<MatchEntity> GetMatches();
        Guid SaveMatch(MatchEntity match);
    }
}