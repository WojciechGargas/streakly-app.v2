using Shouldly;
using Streakly.Core.Entities;
using Streakly.Core.ValueObjects;
using Streakly.Core.ValueObjects.Habit;
using Streakly.Core.ValueObjects.User;

namespace Streakly.Core.UnitTests.Entities;

public class BooleanHabitTests
{
    [Fact]
    public void Ctor_ValidData_CreatesBooleanHabit()
    {
        // Arrange
        var habitId = new HabitId(Guid.NewGuid());
        var userId = new UserId(Guid.NewGuid());
        var habitName = new HabitName("Drink water");
        var frequency = Frequency.EveryDay();
        var createdAtUtc = DateTime.UtcNow;
        DateTime? updatedAtUtc = null;

        // Act
        var habit = new BooleanHabit(
            habitId,
            userId,
            habitName,
            frequency,
            createdAtUtc,
            updatedAtUtc);

        // Assert
        habit.HabitId.ShouldBe(habitId);
        habit.UserId.ShouldBe(userId);
        habit.HabitName.ShouldBe(habitName);
        habit.Frequency.ShouldBe(frequency);
        habit.CreatedAtUtc.ShouldBe(createdAtUtc);
        habit.UpdatedAtUtc.ShouldBeNull();
        habit.Reminders.ShouldBeEmpty();
    }
}