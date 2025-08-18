using Foosball.Application.Dtos;

namespace Foosball.Web;

public class FoosballMatchApiClient
{
    private readonly HttpClient _httpClient;

    public FoosballMatchApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Guid?> StartMatchAsync(StartMatchRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync("api/FoosballMatch/start-match", request, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return null;

        var result = await response.Content.ReadFromJsonAsync<StartMatchResponse>(cancellationToken: cancellationToken);
        return result?.MatchId;
    }

    public async Task<bool> GoalScoredAsync(GoalScoredRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync("api/FoosballMatch/record-goal", request, cancellationToken);
        return response.IsSuccessStatusCode;
    }

    public async Task<GetMatchesResponse?> GetMatchesAsync(CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<GetMatchesResponse>("api/FoosballMatch/get-matches", cancellationToken);
    }

    public async Task<MatchDto?> GetMatchByIdAsync(Guid matchId, CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<MatchDto>($"api/FoosballMatch/get-match-by-id?matchId={matchId}", cancellationToken);
    }

    private class StartMatchResponse
    {
        public Guid MatchId { get; set; }
    }
}