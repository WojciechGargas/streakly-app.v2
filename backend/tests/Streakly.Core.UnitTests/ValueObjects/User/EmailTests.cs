using Shouldly;
using Streakly.Core.Exceptions;
using Streakly.Core.ValueObjects;
using Streakly.Core.ValueObjects.User;

namespace Streakly.Core.UnitTests.ValueObjects.User;

public class EmailTests
{
    [Fact]
    public void Ctor_ValidEmail_CreatesEmail()
    {
        // Arrange
        var value = "user@example.com";

        // Act
        var email = new Email(value);

        // Assert
        email.Value.ShouldBe(value);
    }

    [Fact]
    public void Ctor_UppercaseEmail_NormalizesEmailToLowercase()
    {
        // Arrange
        var value = "USER@EXAMPLE.COM";

        // Act
        var email = new Email(value);

        // Assert
        email.Value.ShouldBe("user@example.com");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("invalid-email")]
    [InlineData("user@example.c")]
    [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa@example.com")]
    public void Ctor_InvalidEmail_ThrowsInvalidEmailException(string value)
    {
        // Act
        var exception = Should.Throw<InvalidEmailException>(() => new Email(value));

        // Assert
        exception.Email.ShouldBe(value.ToLowerInvariant());
    }

    [Fact]
    public void ImplicitOperator_FromString_CreatesEmail()
    {
        // Act
        Email email = "user@example.com";

        // Assert
        email.Value.ShouldBe("user@example.com");
    }

    [Fact]
    public void ImplicitOperator_ToString_ReturnsEmailValue()
    {
        // Arrange
        var email = new Email("user@example.com");

        // Act
        string value = email;

        // Assert
        value.ShouldBe("user@example.com");
    }

    [Fact]
    public void ToString_ReturnsEmailValue()
    {
        // Arrange
        var email = new Email("user@example.com");

        // Act
        var value = email.ToString();

        // Assert
        value.ShouldBe("user@example.com");
    }
}
