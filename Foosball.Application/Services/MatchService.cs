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

        public Guid StartMatch(StartMatchRequest startMatchRequest)
        {
            var players = playerRepository.GetPlayers().Select(player => Player.FromExisting(player.Id, player.Name)).ToList();

            var playerOneDefender = players.GetById(startMatchRequest.TeamOneDefenderId);
            var playerOneAttacker = players.GetById(startMatchRequest.TeamOneAttackerId);
            var playerTwoDefender = players.GetById(startMatchRequest.TeamTwoDefenderId);
            var playerTwoAttacker = players.GetById(startMatchRequest.TeamTwoAttackerId);

            var teamOne = new Team(playerOneDefender, playerOneAttacker);
            var teamTwo = new Team(playerTwoDefender, playerTwoAttacker);

            var match = FoosballMatch.Create(teamOne, teamTwo);
            if (match.IsValid())
            {
                var matchEntity = new MatchEntity { Id = match.Id };
                matchRepository.SaveMatch(matchEntity);
                return match.Id;
            }
            else
            {
                throw new InvalidOperationException("Invalid match configuration.");
            }
        }

        public void RecordGoal(GoalScoredRequest goalScoredRequest)
        {
            goalRepository.AddGoal(new GoalEntity
            {
                MatchId = goalScoredRequest.MatchId,
                ScoringPlayerId = goalScoredRequest.ScoringPlayerId,
                IsOwnGoal = goalScoredRequest.IsOwnGoal,
                Id = Guid.NewGuid(),
                ScoredAt = DateTimeOffset.UtcNow                
            });
        }
    }
}
