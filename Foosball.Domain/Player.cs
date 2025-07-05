namespace Foosball.Domain
{
    public class Player
    {
        private Player(Guid id, string name) 
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public static Player CreateNew(string name)
        {
            var id = Guid.NewGuid();
            if(name is null)
                throw new ArgumentNullException(nameof(name));

            return new Player(id, name);
        }

        public static Player FromExisting(Guid id, string name)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id cannot be empty.", nameof(id));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            return new Player(id, name);
        }
    }
}
