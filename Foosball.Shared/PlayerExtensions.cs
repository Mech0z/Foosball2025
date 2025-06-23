using Foosball.Domain;

namespace Foosball.Shared
{
    public static class PlayerExtensions
    {
        public static Player GetById(this List<Player> items, Guid id)
        {
            return items.SingleOrDefault(item => item.Id == id);
        }
    }
}
