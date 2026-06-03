namespace Streakly.Core.Exceptions;

public class InvalidHabitNameException(string habitName) : CustomException($"Habit name: {habitName} is not a valid habit name.")
{
    public string HabitName { get; } = habitName;
}
