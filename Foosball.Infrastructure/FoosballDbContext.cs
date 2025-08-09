using Foosball.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Foosball.Infrastructure
{
    public class FoosballDbContext : DbContext
    {
        public FoosballDbContext()
        {
            // Default constructor for design-time use, e.g., migrations
            // Configure options here if needed, or leave empty for default behavior
        }

        public FoosballDbContext(DbContextOptions<FoosballDbContext> options) : base(options) { }

        public DbSet<PlayerEntity> Players => Set<PlayerEntity>();
        public DbSet<MatchEntity> Matches => Set<MatchEntity>();
        public DbSet<GoalEntity> Goals => Set<GoalEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MatchEntity>()
                .HasMany(m => m.Goals)
                .WithOne(g => g.Match)
                .HasForeignKey(g => g.MatchId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class FoosballDbContextFactory : IDesignTimeDbContextFactory<FoosballDbContext>
    {
        public FoosballDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FoosballDbContext>();
            // Use your local dev connection string here
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=foosball;User Id=sa;Password=Your_strong_password123!;TrustServerCertificate=True;");

            return new FoosballDbContext(optionsBuilder.Options);
        }
    }
}
