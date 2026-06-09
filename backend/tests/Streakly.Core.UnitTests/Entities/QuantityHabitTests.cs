using Shouldly;
using Streakly.Core.Entities;
using Streakly.Core.Enums;
using Streakly.Core.ValueObjects;
using Streakly.Core.ValueObjects.Habit;
using Streakly.Core.ValueObjects.User;

namespace Streakly.Core.UnitTests.Entities;

public class QuantityHabitTests
{
    [Fact]
    public void Ctor_ValidData_CreatesQuantityHabit()
    {
        // Arrange
        var habitId = new HabitId(Guid.NewGuid());
        var userId = new UserId(Guid.NewGuid());
        var habitName = new HabitName("Walk");
        var frequency = Frequency.EveryDay();
        var quantityGoal = new QuantityGoal(
            10000,
            Unit.Steps,
            GoalOperator.GreaterThanOrEqual);
        var createdAtUtc = DateTime.UtcNow;
        DateTime? updatedAtUtc = null;

        // Act
        var habit = new QuantityHabit(
            habitId,
            userId,
            habitName,
            frequency,
            quantityGoal,
            createdAtUtc,
            updatedAtUtc);

        // Assert
        habit.HabitId.ShouldBe(habitId);
        habit.UserId.ShouldBe(userId);
        habit.HabitName.ShouldBe(habitName);
        habit.Frequency.ShouldBe(frequency);
        habit.QuantityGoal.ShouldBe(quantityGoal);
        habit.CreatedAtUtc.ShouldBe(createdAtUtc);
        habit.UpdatedAtUtc.ShouldBeNull();
        habit.Reminders.ShouldBeEmpty();
    }
}