namespace Foosball.Application.Dtos
{
    public record GoalDto(string PlayerName, bool IsOwnGoal, DateTimeOffset Timestamp);
}
