using Shouldly;
using Streakly.Core.Exceptions;
using Streakly.Core.ValueObjects;
using Streakly.Core.ValueObjects.User;

namespace Streakly.Core.UnitTests.ValueObjects.User;

public class UsernameTests
{
    [Fact]
    public void Ctor_ValidUsername_CreatesUsername()
    {
        // Arrange
        var value = "john";

        // Act
        var username = new Username(value);

        // Assert
        username.Value.ShouldBe(value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("ab")]
    [InlineData("aaaaaaaaaaaaaaaaaaaaa")]
    public void Ctor_InvalidUsername_ThrowsInvalidUsernameException(string value)
    {
        // Act
        var exception = Should.Throw<InvalidUsernameException>(() => new Username(value));

        // Assert
        exception.Username.ShouldBe(value);
    }

    [Fact]
    public void ImplicitOperator_FromString_CreatesUsername()
    {
        // Act
        Username username = "john";

        // Assert
        username.Value.ShouldBe("john");
    }

    [Fact]
    public void ImplicitOperator_ToString_ReturnsUsernameValue()
    {
        // Arrange
        var username = new Username("john");

        // Act
        string value = username;

        // Assert
        value.ShouldBe("john");
    }

    [Fact]
    public void ToString_ReturnsUsernameValue()
    {
        // Arrange
        var username = new Username("john");

        // Act
        var value = username.ToString();

        // Assert
        value.ShouldBe("john");
    }
}
