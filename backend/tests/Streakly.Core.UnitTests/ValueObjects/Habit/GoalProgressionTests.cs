using Shouldly;
using Streakly.Core.Enums;
using Streakly.Core.Exceptions;
using Streakly.Core.ValueObjects.Habit;

namespace Streakly.Core.UnitTests.ValueObjects.Habit;

public class GoalProgressionTests
{
    [Fact]
    public void Ctor_ValidData_CreatesGoalProgression()
    {
        // Arrange
        const decimal changeBy = 1.5m;
        const ProgressionDirection direction = ProgressionDirection.Increase;
        const int every = 2;
        const ProgressionPeriod period = ProgressionPeriod.Weeks;

        // Act
        var progression = new GoalProgression(changeBy, direction, every, period);

        // Assert
        progression.ChangeBy.ShouldBe(changeBy);
        progression.Direction.ShouldBe(direction);
        progression.Every.ShouldBe(every);
        progression.Period.ShouldBe(period);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Ctor_InvalidChangeBy_ThrowsInvalidGoalProgressionException(decimal changeBy)
    {
        // Act
        var exception = Should.Throw<InvalidGoalProgressionException>(() => new GoalProgression(
            changeBy,
            ProgressionDirection.Increase,
            1,
            ProgressionPeriod.Days));

        // Assert
        exception.Message.ShouldBe("Progression change value must be greater than zero.");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Ctor_InvalidEvery_ThrowsInvalidGoalProgressionException(int every)
    {
        // Act
        var exception = Should.Throw<InvalidGoalProgressionException>(() => new GoalProgression(
            1,
            ProgressionDirection.Increase,
            every,
            ProgressionPeriod.Days));

        // Assert
        exception.Message.ShouldBe("Progression interval must be greater than zero.");
    }

    [Fact]
    public void Ctor_InvalidDirection_ThrowsInvalidGoalProgressionException()
    {
        // Arrange
        var direction = (ProgressionDirection)999;

        // Act
        var exception = Should.Throw<InvalidGoalProgressionException>(() => new GoalProgression(
            1,
            direction,
            1,
            ProgressionPeriod.Days));

        // Assert
        exception.Message.ShouldBe("Progression direction: 999 is not valid.");
    }

    [Fact]
    public void Ctor_InvalidPeriod_ThrowsInvalidGoalProgressionException()
    {
        // Arrange
        var period = (ProgressionPeriod)999;

        // Act
        var exception = Should.Throw<InvalidGoalProgressionException>(() => new GoalProgression(
            1,
            ProgressionDirection.Increase,
            1,
            period));

        // Assert
        exception.Message.ShouldBe("Progression period: 999 is not valid.");
    }
}
