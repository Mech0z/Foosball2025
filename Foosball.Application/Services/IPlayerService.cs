using Foosball.Application.Dtos;

namespace Foosball.Application.Services
{
    public interface IPlayerService
    {
        Task<Guid> CreatePlayerAsync(string name);
        Task<List<PlayerDto>> GetPlayersAsync();
    }
}