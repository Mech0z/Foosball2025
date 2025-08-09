namespace Foosball.Application.Dtos
{
    public record TeamDto(string Defender, string Attacker)
    {
        public TeamDto() : this(string.Empty, string.Empty) { }
        public TeamDto(string playerName)
            : this(playerName, playerName) { }
    }
}