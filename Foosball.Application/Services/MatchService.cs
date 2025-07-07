using Domain;
using Foosball.Application.Dtos;
using Foosball.Domain;
using Foosball.Infrastructure.Entities;
using Foosball.Infrastructure.Repositories;
using Foosball.Shared;

namespace Foosball.Application.Services
{
    public class MatchService : IMatchService
    {
        private readonly IPlayerRepository playerRepository;
        private readonly IMatchRepository matchRepository;
        private readonly IGoalRepository goalRepository;

        public MatchService(IPlayerRepository playerRepository, IMatchRepository matchRepository, IGoalRepository goalRepository)
        {
            this.playerRepository = playerRepository;
            this.matchRepository = matchRepository;
            this.goalRepository = goalRepository;
        }

        public async Task<Guid> StartMatch(StartMatchRequest startMatchRequest)
        {
            var players = await playerRepository.GetPlayers();
            var mappedPlayers = players.Select(player => Player.FromExisting(player.Id, player.Name)).ToList();

            var playerOneDefender = mappedPlayers.GetById(startMatchRequest.TeamOneDefenderId);
            var playerOneAttacker = mappedPlayers.GetById(startMatchRequest.TeamOneAttackerId);
            var playerTwoDefender = mappedPlayers.GetById(startMatchRequest.TeamTwoDefenderId);
            var playerTwoAttacker = mappedPlayers.GetById(startMatchRequest.TeamTwoAttackerId);

            var teamOne = new Team(playerOneDefender, playerOneAttacker);
            var teamTwo = new Team(playerTwoDefender, playerTwoAttacker);

            var match = FoosballMatch.Create(teamOne, teamTwo);
            if (match.IsValid())
            {
                var matchEntity = new MatchEntity { 
                    Id = match.Id, 
                    Team1DefenderId = playerOneDefender.Id, 
                    Team1AttackerId = playerOneAttacker.Id, 
                    Team2DefenderId = playerTwoDefender.Id,
                    Team2AttackerId = playerTwoAttacker.Id,
                    CreatedAt = DateTimeOffset.UtcNow,
                    FinishedAt = null,
                };
                await matchRepository.SaveMatch(matchEntity);
                return match.Id;
            }
            else
            {
                throw new InvalidOperationException("Invalid match configuration.");
            }
        }

        public async Task RecordGoal(GoalScoredRequest goalScoredRequest)
        {
            await goalRepository.AddGoal(new GoalEntity
            {
                MatchId = goalScoredRequest.MatchId,
                ScoringPlayerId = goalScoredRequest.ScoringPlayerId,
                IsOwnGoal = goalScoredRequest.IsOwnGoal,
                Id = Guid.NewGuid(),
                ScoredAt = DateTimeOffset.UtcNow                
            });
        }

        public async Task<GetMatchesResponse> GetMatches()
        {
            var matches = await matchRepository.GetMatches();
            var players = await playerRepository.GetPlayers();
            var mappedPlayers = players.Select(player => Player.FromExisting(player.Id, player.Name)).ToList();
            var matchDtos = matches.Select(match => new MatchDto
            (
                match.Id,
                new TeamDto(mappedPlayers.GetById(match.Team1DefenderId).Name, mappedPlayers.GetById(match.Team1AttackerId).Name),
                new TeamDto(mappedPlayers.GetById(match.Team2DefenderId).Name, mappedPlayers.GetById(match.Team2AttackerId).Name),
                match.Goals.Count,
                match.Goals.Count
            )).ToList();
            return new GetMatchesResponse(matchDtos);
        }
    }
}
