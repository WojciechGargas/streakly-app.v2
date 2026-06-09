using Streakly.Core.ValueObjects;
using Streakly.Core.ValueObjects.Habit;
using Streakly.Core.ValueObjects.User;

namespace Streakly.Core.Entities;

public sealed class QuantityHabit(
    HabitId habitId,
    UserId userId,
    HabitName habitName,
    Frequency frequency,
    QuantityGoal quantityGoal,
    DateTime createdAtUtc,
    DateTime? updatedAtUtc = null)
    : Habit(
        habitId,
        userId,
        habitName,
        frequency,
        createdAtUtc,
        updatedAtUtc)
{
    public QuantityGoal QuantityGoal { get; private set; } = quantityGoal;
}