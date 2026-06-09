using Shouldly;
using Streakly.Core.ValueObjects.Habit;

namespace Streakly.Core.UnitTests.ValueObjects.Habit;

public class ReminderTests
{
    [Fact]
    public void Ctor_ValidTime_CreatesReminder()
    {
        // Arrange
        var time = TimeSpan.FromHours(8);

        // Act
        var reminder = new Reminder(time);

        // Assert
        reminder.Time.ShouldBe(time);
    }
}
