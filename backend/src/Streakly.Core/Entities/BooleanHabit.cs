using Streakly.Core.ValueObjects;
using Streakly.Core.ValueObjects.Habit;
using Streakly.Core.ValueObjects.User;

namespace Streakly.Core.Entities;

public sealed class BooleanHabit(
    HabitId habitId,
    UserId userId,
    HabitName habitName,
    Frequency frequency,
    DateTime createdAtUtc,
    DateTime? updatedAtUtc = null)
    : Habit(habitId,
        userId,
        habitName,
        frequency,
        createdAtUtc,
        updatedAtUtc);