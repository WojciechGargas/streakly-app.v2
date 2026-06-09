using Shouldly;
using Streakly.Core.Exceptions;
using Streakly.Core.ValueObjects.Habit;

namespace Streakly.Core.UnitTests.ValueObjects.Habit;

public class HabitIdTests
{
    [Fact]
    public void Ctor_ValidGuid_CreatesHabitId()
    {
        // Arrange
        var value = Guid.NewGuid();

        // Act
        var habitId = new HabitId(value);

        // Assert
        habitId.Value.ShouldBe(value);
    }

    [Fact]
    public void Ctor_EmptyGuid_ThrowsInvalidEntityException()
    {
        // Act
        var exception = Should.Throw<InvalidEntityException>(() => new HabitId(Guid.Empty));

        // Assert
        exception.Id.ShouldBe(Guid.Empty);
    }

    [Fact]
    public void ImplicitOperator_FromGuid_CreatesHabitId()
    {
        // Arrange
        var value = Guid.NewGuid();

        // Act
        HabitId habitId = value;

        // Assert
        habitId.Value.ShouldBe(value);
    }

    [Fact]
    public void ImplicitOperator_ToGuid_ReturnsHabitIdValue()
    {
        // Arrange
        var value = Guid.NewGuid();
        var habitId = new HabitId(value);

        // Act
        Guid result = habitId;

        // Assert
        result.ShouldBe(value);
    }
}
