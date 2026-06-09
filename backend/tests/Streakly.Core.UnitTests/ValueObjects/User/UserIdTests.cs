using Shouldly;
using Streakly.Core.Exceptions;
using Streakly.Core.ValueObjects;
using Streakly.Core.ValueObjects.User;

namespace Streakly.Core.UnitTests.ValueObjects.User;

public class UserIdTests
{
    [Fact]
    public void Ctor_ValidGuid_CreatesUserId()
    {
        // Arrange
        var value = Guid.NewGuid();

        // Act
        var userId = new UserId(value);

        // Assert
        userId.Value.ShouldBe(value);
    }

    [Fact]
    public void Ctor_EmptyGuid_ThrowsInvalidEntityException()
    {
        // Act
        var exception = Should.Throw<InvalidEntityException>(() => new UserId(Guid.Empty));

        // Assert
        exception.Id.ShouldBe(Guid.Empty);
    }

    [Fact]
    public void ImplicitOperator_FromGuid_CreatesUserId()
    {
        // Arrange
        var value = Guid.NewGuid();

        // Act
        UserId userId = value;

        // Assert
        userId.Value.ShouldBe(value);
    }

    [Fact]
    public void ImplicitOperator_ToGuid_ReturnsUserIdValue()
    {
        // Arrange
        var value = Guid.NewGuid();
        var userId = new UserId(value);

        // Act
        Guid result = userId;

        // Assert
        result.ShouldBe(value);
    }
}
