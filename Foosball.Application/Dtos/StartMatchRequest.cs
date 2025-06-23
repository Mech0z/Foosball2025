namespace Foosball.Application.Dtos
{
    public class StartMatchRequest
    {
        public Guid TeamOneAttackerId { get; set; }
        public Guid TeamOneDefenderId { get; set; }
        public Guid TeamTwoAttackerId { get; set; }
        public Guid TeamTwoDefenderId { get; set; }
    }
}
