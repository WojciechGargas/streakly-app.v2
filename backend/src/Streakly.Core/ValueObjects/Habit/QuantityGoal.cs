using Streakly.Core.Enums;
using Streakly.Core.Exceptions;

namespace Streakly.Core.ValueObjects.Habit;

public record QuantityGoal
{
    public decimal InitialTarget { get; }
    public Unit Unit { get; }
    public GoalOperator GoalOperator { get; }
    public GoalProgression? Progression { get; }

    public QuantityGoal(
        decimal initialTarget,
        Unit unit,
        GoalOperator goalOperator,
        GoalProgression? progression = null)
    {
        if (initialTarget <= 0)
        {
            throw new InvalidInitialTargetException();
        }

        InitialTarget = initialTarget;
        Unit = unit;
        GoalOperator = goalOperator;
        Progression = progression;
    }
}
