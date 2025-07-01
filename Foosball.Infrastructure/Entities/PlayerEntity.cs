using System.ComponentModel.DataAnnotations.Schema;

namespace Foosball.Infrastructure.Entities
{
    [Table("Players")]
    public class PlayerEntity
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }
}
