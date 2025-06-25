using Foosball.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Foosball.Infrastructure
{
    public class FoosballDbContext : DbContext
    {
        public FoosballDbContext(DbContextOptions<FoosballDbContext> options) : base(options) { }

        public DbSet<PlayerEntity> Players => Set<PlayerEntity>();
        public DbSet<MatchEntity> Matches => Set<MatchEntity>();
        public DbSet<GoalEntity> Goals => Set<GoalEntity>();
    }
}
