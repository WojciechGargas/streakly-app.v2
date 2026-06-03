using Streakly.Core.Exceptions;

namespace Streakly.Core.ValueObjects.Habit;

public record HabitId
{
    public Guid Value { get; }

    public HabitId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidEntityException(value);
        }

        Value = value;
    }

    public static implicit operator HabitId(Guid value) => new HabitId(value);

    public static implicit operator Guid(HabitId habitId) => habitId.Value;
}
