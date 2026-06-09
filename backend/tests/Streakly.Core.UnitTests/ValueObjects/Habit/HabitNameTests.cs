using Shouldly;
using Streakly.Core.Exceptions;
using Streakly.Core.ValueObjects.Habit;

namespace Streakly.Core.UnitTests.ValueObjects.Habit;

public class HabitNameTests
{
    [Fact]
    public void Ctor_ValidHabitName_CreatesHabitName()
    {
        // Arrange
        var value = "Drink water";

        // Act
        var habitName = new HabitName(value);

        // Assert
        habitName.Value.ShouldBe(value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
    public void Ctor_InvalidHabitName_ThrowsInvalidHabitNameException(string value)
    {
        // Act
        var exception = Should.Throw<InvalidHabitNameException>(() => new HabitName(value));

        // Assert
        exception.HabitName.ShouldBe(value);
    }

    [Fact]
    public void ImplicitOperator_FromString_CreatesHabitName()
    {
        // Act
        HabitName habitName = "Drink water";

        // Assert
        habitName.Value.ShouldBe("Drink water");
    }

    [Fact]
    public void ImplicitOperator_ToString_ReturnsHabitNameValue()
    {
        // Arrange
        var habitName = new HabitName("Drink water");

        // Act
        string value = habitName;

        // Assert
        value.ShouldBe("Drink water");
    }

    [Fact]
    public void ToString_ReturnsHabitNameValue()
    {
        // Arrange
        var habitName = new HabitName("Drink water");

        // Act
        var value = habitName.ToString();

        // Assert
        value.ShouldBe("Drink water");
    }
}
