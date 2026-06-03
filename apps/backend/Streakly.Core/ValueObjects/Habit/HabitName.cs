using Streakly.Core.Exceptions;

namespace Streakly.Core.ValueObjects.Habit;

public record HabitName
{
    public string Value { get; }

    public HabitName(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length is < 1 or > 32)
        {
            throw new InvalidHabitNameException(value);
        }

        Value = value;
    }

    public static implicit operator HabitName(string value) => new HabitName(value);

    public static implicit operator string(HabitName value) => value.Value;

    public override string ToString() => Value;
}
