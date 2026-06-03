using Streakly.Core.Enums;
using Streakly.Core.Exceptions;

namespace Streakly.Core.ValueObjects.Habit;

public sealed record GoalProgression
{
    public decimal ChangeBy { get; }
    public ProgressionDirection Direction { get; }
    public int Every { get; }
    public ProgressionPeriod Period { get; }

    public GoalProgression(
        decimal changeBy,
        ProgressionDirection direction,
        int every,
        ProgressionPeriod period)
    {
        if (changeBy <= 0)
        {
            throw new InvalidGoalProgressionException(
                "Progression change value must be greater than zero.");
        }

        if (every <= 0)
        {
            throw new InvalidGoalProgressionException(
                "Progression interval must be greater than zero.");
        }

        if (!Enum.IsDefined(direction))
        {
            throw new InvalidGoalProgressionException(
                $"Progression direction: {direction} is not valid.");
        }

        if (!Enum.IsDefined(period))
        {
            throw new InvalidGoalProgressionException(
                $"Progression period: {period} is not valid.");
        }

        ChangeBy = changeBy;
        Direction = direction;
        Every = every;
        Period = period;
    }
}