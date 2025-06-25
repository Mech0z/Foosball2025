using Foosball.Infrastructure.Entities;

namespace Foosball.Infrastructure.Repositories
{
    public interface IGoalRepository
    {
        Task<Guid> AddGoal(GoalEntity goal);
        Task<List<GoalEntity>> GetGoals(Guid matchId);
    }
}