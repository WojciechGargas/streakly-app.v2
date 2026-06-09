using Shouldly;
using Streakly.Core.Entities;
using Streakly.Core.Enums;
using Streakly.Core.ValueObjects;
using Streakly.Core.ValueObjects.User;

namespace Streakly.Core.UnitTests.Entities;

public class UserTests
{
    [Fact]
    public void Ctor_ValidData_CreatesUser()
    {
        //Arrange
        var userId = new UserId(Guid.NewGuid());
        var email = new Email("user@example.com");
        var username = new Username("user");
        var password = new Password("password");
        var fullname = new Fullname("fullname example");
        var role = UserRole.User;
        var createdAt = DateTime.UtcNow;
        DateTime? lastLoggedAtUtc = null;
        
        // Act
        var user = new User(
            userId,
            email,
            username,
            password,
            fullname,
            role,
            createdAt,
            lastLoggedAtUtc);

        // Assert
        user.UserId.ShouldBe(userId);
        user.Email.ShouldBe(email);
        user.Username.ShouldBe(username);
        user.Password.ShouldBe(password);
        user.Fullname.ShouldBe(fullname);
        user.Role.ShouldBe(role);
        user.CreatedAt.ShouldBe(createdAt);
        user.LastLoggedAtUtc.ShouldBeNull();
        user.IsEmailConfirmed.ShouldBeFalse();
    }
}