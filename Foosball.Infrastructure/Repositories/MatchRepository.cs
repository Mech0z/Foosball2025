using Foosball.Infrastructure.Entities;

namespace Foosball.Infrastructure.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        public MatchRepository() { }

        public List<MatchEntity> GetMatches()
        {
            // This method should return a list of matches.
            // For now, we return an empty list as a placeholder.
            return new List<MatchEntity>();
        }

        public MatchEntity GetMatchById(Guid matchId)
        {
            // This method should return a match by its ID.
            // For now, we return null as a placeholder.
            return null;
        }

        public Guid SaveMatch(MatchEntity match)
        {
            // This method should save the match to the database.
            // For now, we do nothing as a placeholder.
            return Guid.NewGuid();
        }
    }
}
