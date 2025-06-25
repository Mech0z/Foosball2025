using Foosball.Infrastructure.Entities;

namespace Foosball.Infrastructure.Repositories
{
    public class GoalRepository : IGoalRepository
    {
        public GoalRepository() { }

        public List<GoalEntity> GetGoals(Guid matchId)
        {
            // This method should return a list of goals for the specified match.
            // For now, we return an empty list as a placeholder.
            return new List<GoalEntity>();
        }

        public void AddGoal(GoalEntity goal)
        {
            // This method should add a goal to the repository.
            // For now, we do nothing as a placeholder.
        }
    }
}
