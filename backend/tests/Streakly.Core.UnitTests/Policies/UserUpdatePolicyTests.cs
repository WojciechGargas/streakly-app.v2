using Shouldly;
using Streakly.Core.Entities;
using Streakly.Core.Enums;
using Streakly.Core.Policies;
using Streakly.Core.ValueObjects.User;

namespace Streakly.Core.UnitTests.Policies;

public class UserUpdatePolicyTests
{
    private readonly UserUpdatePolicy _policy = new();

    [Fact]
    public void CanUpdate_RequestedByIsUserToUpdate_ReturnsTrue()
    {
        // Arrange
        var user = CreateUser(UserRole.User);

        // Act
        var result = _policy.CanUpdate(user, user);

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void CanUpdate_RequestedByIsAdmin_ReturnsTrue()
    {
        // Arrange
        var admin = CreateUser(UserRole.Admin, "admin@example.com", "admin");
        var userToUpdate = CreateUser(UserRole.User, "user@example.com", "user");

        // Act
        var result = _policy.CanUpdate(admin, userToUpdate);

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void CanUpdate_RequestedByIsDifferentRegularUser_ReturnsFalse()
    {
        // Arrange
        var requestedBy = CreateUser(UserRole.User, "user1@example.com", "user1");
        var userToUpdate = CreateUser(UserRole.User, "user2@example.com", "user2");

        // Act
        var result = _policy.CanUpdate(requestedBy, userToUpdate);

        // Assert
        result.ShouldBeFalse();
    }

    private static User CreateUser(
        UserRole role,
        string email = "user@example.com",
        string username = "user")
    {
        return new User(
            new UserId(Guid.NewGuid()),
            new Email(email),
            new Username(username),
            new Password("password"),
            new Fullname("John Doe"),
            role,
            DateTime.UtcNow,
            null);
    }
}
