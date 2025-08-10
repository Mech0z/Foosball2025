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

        private Team CreateTeam(List<Player> players, Guid defenderId, Guid attackerId)
        {
            var defender = players.FirstOrDefault(p => p.Id == defenderId);
            var attacker = players.FirstOrDefault(p => p.Id == attackerId);
            if (defender == null || attacker == null)
            {
                throw new ArgumentException("Invalid player IDs provided for team creation.");
            }
            return new Team(Player.FromExisting(defender.Id, defender.Name), Player.FromExisting(attacker.Id, attacker.Name));
        }

        public async Task<GetMatchesResponse> GetMatches()
        {
            var matches = await matchRepository.GetMatches();
            var players = await playerRepository.GetPlayers();
            var mappedPlayers = players.Select(player => Player.FromExisting(player.Id, player.Name)).ToList();

            var foosballMatches = matches.Select(match => FoosballMatch.FromExisting(match.Id, CreateTeam(mappedPlayers, match.Team1DefenderId, match.Team1AttackerId), 
                                                                                  CreateTeam(mappedPlayers, match.Team2DefenderId, match.Team2AttackerId), 
                                                                                  match.Goals.Select(goal => FoosballGoal.FromExisting(goal.Id, goal.ScoringPlayerId, goal.IsOwnGoal, goal.ScoredAt)).ToList(), 
                                                                                  match.FinishedAt.HasValue)).ToList();
            var matchDtos = foosballMatches.Select(match => new MatchDto
            (
                match.Id,
                new TeamDto(match.TeamA.Defender.Name, match.TeamA.Attacker.Name),
                new TeamDto(match.TeamB.Defender.Name, match.TeamB.Attacker.Name),
                match.GetTeamAScore(),
                match.GetTeamBScore()
            )).ToList();
            return new GetMatchesResponse(matchDtos);
        }
    }
}
