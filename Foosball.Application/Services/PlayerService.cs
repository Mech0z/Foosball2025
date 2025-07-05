using Foosball.Application.Dtos;
using Foosball.Domain;
using Foosball.Infrastructure.Entities;
using Foosball.Infrastructure.Repositories;

namespace Foosball.Application.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            this.playerRepository = playerRepository;
        }

        public async Task<Guid> CreatePlayerAsync(string name)
        {
            var player = Player.CreateNew(name);
            var entity = new PlayerEntity
            {
                Id = player.Id,
                Name = player.Name
            };
            await playerRepository.AddPlayer(entity);
            return player.Id;
        }

        public async Task<List<PlayerDto>> GetPlayersAsync()
        {
            var players = await playerRepository.GetPlayers();
            return players.Select(p => new PlayerDto
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();
        }
    }
}
