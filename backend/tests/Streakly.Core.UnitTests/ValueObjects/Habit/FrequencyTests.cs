using Shouldly;
using Streakly.Core.Exceptions;
using Streakly.Core.ValueObjects.Habit;

namespace Streakly.Core.UnitTests.ValueObjects.Habit;

public class FrequencyTests
{
    [Fact]
    public void Ctor_ValidDays_CreatesFrequency()
    {
        // Arrange
        var days = new[] { DayOfWeek.Monday, DayOfWeek.Wednesday };

        // Act
        var frequency = new Frequency(days);

        // Assert
        frequency.Days.ShouldBe(days.ToHashSet());
    }

    [Fact]
    public void Ctor_DuplicateDays_KeepsUniqueDays()
    {
        // Arrange
        var days = new[] { DayOfWeek.Monday, DayOfWeek.Monday };

        // Act
        var frequency = new Frequency(days);

        // Assert
        frequency.Days.Count.ShouldBe(1);
        frequency.Days.ShouldContain(DayOfWeek.Monday);
    }

    [Fact]
    public void Ctor_EmptyDays_ThrowsInvalidFrequencyException()
    {
        // Act & Assert
        Should.Throw<InvalidFrequencyException>(() => new Frequency(Array.Empty<DayOfWeek>()));
    }

    [Fact]
    public void Includes_DateWithConfiguredDay_ReturnsTrue()
    {
        // Arrange
        var frequency = new Frequency(new[] { DayOfWeek.Monday });
        var monday = new DateOnly(2026, 6, 8);

        // Act
        var result = frequency.Includes(monday);

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void Includes_DateWithoutConfiguredDay_ReturnsFalse()
    {
        // Arrange
        var frequency = new Frequency(new[] { DayOfWeek.Monday });
        var tuesday = new DateOnly(2026, 6, 9);

        // Act
        var result = frequency.Includes(tuesday);

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void EveryDay_CreatesFrequencyWithAllDays()
    {
        // Act
        var frequency = Frequency.EveryDay();

        // Assert
        frequency.Days.ShouldBe(Enum.GetValues<DayOfWeek>().ToHashSet());
    }
}
