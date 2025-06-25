using Foosball.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Foosball.Infrastructure.Repositories
{
    public class GoalRepository : IGoalRepository
    {
        private readonly FoosballDbContext _dbContext;

        public GoalRepository(FoosballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<GoalEntity>> GetGoals(Guid matchId)
        {
            var goals = await _dbContext.Goals.AsNoTracking().ToListAsync();
            return goals;
        }

        public async Task<Guid> AddGoal(GoalEntity goal)
        {
            await _dbContext.Goals.AddAsync(goal);
            var goalId = await _dbContext.SaveChangesAsync().ContinueWith(t => goal.Id);
            return goalId;
        }
    }
}