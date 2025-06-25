using Foosball.Infrastructure.Entities;

namespace Foosball.Infrastructure.Repositories
{
    public interface IGoalRepository
    {
        void AddGoal(GoalEntity goal);
        List<GoalEntity> GetGoals(Guid matchId);
    }
}