namespace Foosball.Infrastructure.Entities
{
    public class PlayerEntity
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }
}
