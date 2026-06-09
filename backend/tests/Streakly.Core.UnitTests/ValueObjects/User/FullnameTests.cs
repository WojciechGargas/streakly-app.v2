using Shouldly;
using Streakly.Core.Exceptions;
using Streakly.Core.ValueObjects;
using Streakly.Core.ValueObjects.User;

namespace Streakly.Core.UnitTests.ValueObjects.User;

public class FullnameTests
{
    [Fact]
    public void Ctor_ValidFullname_CreatesFullname()
    {
        // Arrange
        var value = "John Doe";

        // Act
        var fullname = new Fullname(value);

        // Assert
        fullname.Value.ShouldBe(value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
    public void Ctor_InvalidFullname_ThrowsInvalidFullnameException(string value)
    {
        // Act
        var exception = Should.Throw<InvalidFullnameException>(() => new Fullname(value));

        // Assert
        exception.Fullname.ShouldBe(value);
    }

    [Fact]
    public void ImplicitOperator_FromString_CreatesFullname()
    {
        // Act
        Fullname fullname = "John Doe";

        // Assert
        fullname.Value.ShouldBe("John Doe");
    }

    [Fact]
    public void ImplicitOperator_ToString_ReturnsFullnameValue()
    {
        // Arrange
        var fullname = new Fullname("John Doe");

        // Act
        string value = fullname;

        // Assert
        value.ShouldBe("John Doe");
    }

    [Fact]
    public void ToString_ReturnsFullnameValue()
    {
        // Arrange
        var fullname = new Fullname("John Doe");

        // Act
        var value = fullname.ToString();

        // Assert
        value.ShouldBe("John Doe");
    }
}
