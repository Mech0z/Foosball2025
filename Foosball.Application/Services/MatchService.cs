using Domain;
using Foosball.Application.Dtos;
using Foosball.Domain;
using Foosball.Infrastructure.Repositories;
using Foosball.Shared;

namespace Foosball.Application.Services
{
    public class MatchService : IMatchService
    {
        private readonly IPlayerRepository playerRepository;

        public MatchService(IPlayerRepository playerRepository)
        {
            this.playerRepository = playerRepository;
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

            var match = new FoosballMatch(teamOne, teamTwo);
            if (match.IsValid())
            {
                // Assuming there's a repository to save the match
                // matchRepository.Save(match);
                return match.Id;
            }
            else
            {
                throw new InvalidOperationException("Invalid match configuration.");
            }
        }
    }
}
