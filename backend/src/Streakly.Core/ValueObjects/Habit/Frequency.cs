using Streakly.Core.Exceptions;

namespace Streakly.Core.ValueObjects.Habit;

public sealed record Frequency
{
    public IReadOnlySet<DayOfWeek> Days { get; }

    public Frequency(IEnumerable<DayOfWeek> days)
    {
        var uniqueDays = days.ToHashSet();

        if (uniqueDays.Count == 0)
        {
            throw new InvalidFrequencyException();
        }

        Days = uniqueDays;
    }

    public bool Includes(DateOnly date)
        => Days.Contains(date.DayOfWeek);

    public static Frequency EveryDay()
        => new Frequency(Enum.GetValues<DayOfWeek>());
}
