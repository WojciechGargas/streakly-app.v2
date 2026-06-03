using Streakly.Core.Enums;
using Streakly.Core.ValueObjects;
using Streakly.Core.ValueObjects.Habit;

namespace Streakly.Core.Entities;

public abstract class Habit(
    HabitId habitId,
    UserId userId,
    HabitName habitName,
    Frequency frequency,
    DateTime createdAtUtc,
    DateTime? updatedAtUtc)
{
    public HabitId HabitId { get; private set; } = habitId;
    public UserId UserId { get; private set; } = userId;
    public HabitName HabitName { get; private set; } = habitName;
    public Frequency Frequency { get; private set; } = frequency;
    public DateTime CreatedAtUtc { get; private set; } = createdAtUtc;
    public DateTime? UpdatedAtUtc { get; private set; } = updatedAtUtc;

    private readonly List<Reminder> _reminders = [];
    public IReadOnlyCollection<Reminder> Reminders => _reminders;
}