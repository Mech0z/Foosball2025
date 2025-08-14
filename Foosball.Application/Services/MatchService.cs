using Domain;
using Foosball.Application.Dtos;
using Foosball.Domain;
using Foosball.Infrastructure.Entities;
using Foosball.Infrastructure.Repositories;

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

            var teamOne = CreateTeam(mappedPlayers, startMatchRequest.TeamOneDefenderId, startMatchRequest.TeamOneAttackerId);
            var teamTwo = CreateTeam(mappedPlayers, startMatchRequest.TeamTwoDefenderId, startMatchRequest.TeamTwoAttackerId);

            var match = FoosballMatch.Create(teamOne, teamTwo);
            if (match.IsValid())
            {
                var matchEntity = MatchEntity.FromDomain(match);
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
            var match = await matchRepository.GetMatchById(goalScoredRequest.MatchId);
            if (match == null)
            {
                throw new KeyNotFoundException($"Match with ID {goalScoredRequest.MatchId} not found.");
            }

            var team1 = new Team(
                Player.FromExisting(match.Team1Defender.Id, match.Team1Defender.Name),
                Player.FromExisting(match.Team1Attacker.Id, match.Team1Attacker.Name)
            );
            var team2 = new Team(
                Player.FromExisting(match.Team2Defender.Id, match.Team2Defender.Name),
                Player.FromExisting(match.Team2Attacker.Id, match.Team2Attacker.Name)
            );

            var goals = match.Goals.Select(goal => FoosballGoal.FromExisting(goal.Id, goal.ScoringPlayerId, goal.IsOwnGoal, goal.Timestamp)).ToList();

            var foosballMatch = FoosballMatch.FromExisting(match.Id, team1, team2, goals);
            var goal = foosballMatch.RecordGoal(goalScoredRequest.ScoringPlayerId, goalScoredRequest.IsOwnGoal);

            var goalEntity = GoalEntity.FromDomain(foosballMatch.Id, goal);
            await goalRepository.AddGoal(goalEntity);
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

            var matchDtos = matches
                .Select(match => MatchDto.FromDomain(match.ToDomain(mappedPlayers), mappedPlayers))
                .ToList();

            return new GetMatchesResponse(matchDtos);
        }

        public async Task<MatchDto> GetMatchById(Guid matchId)
        {
            var match = await matchRepository.GetMatchById(matchId);
            if (match == null)
            {
                throw new KeyNotFoundException($"Match with ID {matchId} not found.");
            }
            var players = await playerRepository.GetPlayers();
            var mappedPlayers = players.Select(player => Player.FromExisting(player.Id, player.Name)).ToList();

            return MatchDto.FromDomain(match.ToDomain(mappedPlayers), mappedPlayers);
        }
    }
}
