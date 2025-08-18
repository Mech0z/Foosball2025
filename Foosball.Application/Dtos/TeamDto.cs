namespace Foosball.Application.Dtos
{
    public record TeamDto(PlayerDto Defender, PlayerDto Attacker)
    {
        public TeamDto() : this(new PlayerDto { Name = string.Empty }, new PlayerDto { Name = string.Empty }) { }
        public TeamDto(PlayerDto player)
            : this(player, player) { }
    }
}