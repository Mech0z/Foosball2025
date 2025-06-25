using Foosball.Application.Dtos;

namespace Foosball.Application.Services
{
    public interface IMatchService
    {
        Task<Guid> StartMatch(StartMatchRequest startMatchRequest);
    }
}
