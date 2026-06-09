using Shouldly;
using Streakly.Core.Exceptions;
using Streakly.Core.ValueObjects;
using Streakly.Core.ValueObjects.User;

namespace Streakly.Core.UnitTests.ValueObjects.User;

public class PasswordTests
{
    [Fact]
    public void Ctor_ValidPassword_CreatesPassword()
    {
        // Arrange
        var value = "secret-password";

        // Act
        var password = new Password(value);

        // Assert
        password.Value.ShouldBe(value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("12345")]
    public void Ctor_InvalidPassword_ThrowsInvalidPasswordException(string value)
    {
        // Act
        var exception = Should.Throw<InvalidPasswordException>(() => new Password(value));

        // Assert
        exception.Password.ShouldBe(value);
    }
    
    [Fact]
    public void Ctor_TooLongPassword_ThrowsInvalidPasswordException()
    {
        // Arrange
        var value = new string('a', 101);

        // Act
        var exception = Should.Throw<InvalidPasswordException>(() => new Password(value));

        // Assert
        exception.Password.ShouldBe(value);
    }

    [Fact]
    public void ImplicitOperator_FromString_CreatesPassword()
    {
        // Act
        Password password = "secret-password";

        // Assert
        password.Value.ShouldBe("secret-password");
    }

    [Fact]
    public void ImplicitOperator_ToString_ReturnsPasswordValue()
    {
        // Arrange
        var password = new Password("secret-password");

        // Act
        string value = password;

        // Assert
        value.ShouldBe("secret-password");
    }

    [Fact]
    public void ToString_ReturnsPasswordValue()
    {
        // Arrange
        var password = new Password("secret-password");

        // Act
        var value = password.ToString();

        // Assert
        value.ShouldBe("secret-password");
    }
}
