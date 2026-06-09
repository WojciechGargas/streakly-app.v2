using Shouldly;
using Streakly.Core.Enums;
using Streakly.Core.Exceptions;
using Streakly.Core.ValueObjects.Habit;

namespace Streakly.Core.UnitTests.ValueObjects.Habit;

public class QuantityGoalTests
{
    [Fact]
    public void Ctor_ValidDataWithoutProgression_CreatesQuantityGoal()
    {
        // Arrange
        const decimal initialTarget = 10000;
        const Unit unit = Unit.Steps;
        const GoalOperator goalOperator = GoalOperator.GreaterThanOrEqual;

        // Act
        var quantityGoal = new QuantityGoal(initialTarget, unit, goalOperator);

        // Assert
        quantityGoal.InitialTarget.ShouldBe(initialTarget);
        quantityGoal.Unit.ShouldBe(unit);
        quantityGoal.GoalOperator.ShouldBe(goalOperator);
        quantityGoal.Progression.ShouldBeNull();
    }

    [Fact]
    public void Ctor_ValidDataWithProgression_CreatesQuantityGoal()
    {
        // Arrange
        var progression = new GoalProgression(1, ProgressionDirection.Increase, 1, ProgressionPeriod.Weeks);

        // Act
        var quantityGoal = new QuantityGoal(10, Unit.Kg, GoalOperator.LessThanOrEqual, progression);

        // Assert
        quantityGoal.InitialTarget.ShouldBe(10);
        quantityGoal.Unit.ShouldBe(Unit.Kg);
        quantityGoal.GoalOperator.ShouldBe(GoalOperator.LessThanOrEqual);
        quantityGoal.Progression.ShouldBe(progression);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Ctor_InvalidInitialTarget_ThrowsInvalidInitialTargetException(decimal initialTarget)
    {
        // Act & Assert
        Should.Throw<InvalidInitialTargetException>(() => new QuantityGoal(
            initialTarget,
            Unit.Steps,
            GoalOperator.GreaterThanOrEqual));
    }
}
