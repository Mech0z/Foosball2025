namespace Foosball.Application.Dtos
{
    public class GetMatchesResponse
    {
        public List<MatchDto> Matches { get; set; } = new List<MatchDto>();
        public GetMatchesResponse()
        {
        }
        public GetMatchesResponse(List<MatchDto> matches)
        {
            Matches = matches ?? new List<MatchDto>();
        }
    }
}
