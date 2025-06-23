using Foosball.Application.Dtos;

namespace Foosball.Application.Services
{
    public interface IMatchService
    {
        Guid StartMatch(StartMatchRequest startMatchRequest);
    }
}
