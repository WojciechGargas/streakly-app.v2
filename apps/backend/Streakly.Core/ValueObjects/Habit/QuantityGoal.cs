using Streakly.Core.Enums;

namespace Streakly.Core.ValueObjects.Habit;

public record QuantityGoal
{
    public decimal InitialTarget { get; }
    public Unit Unit { get; }
    public GoalOperator GoalOperator { get; }
    public ProgressionDirection? ProgressionDirection { get; }
}