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
    
    [Fact]
    public void MarkAsLoggedIn_ValidDate_UpdatesLastLoggedAtUtc()
    {
        // Arrange
        var user = CreateUser();
        var loggedAtUtc = DateTime.UtcNow;

        // Act
        user.MarkAsLoggedIn(loggedAtUtc);

        // Assert
        user.LastLoggedAtUtc.ShouldBe(loggedAtUtc);
    }

    [Fact]
    public void MarkEmailAsConfirmed_UserEmailNotConfirmed_SetsEmailAsConfirmed()
    {
        // Arrange
        var user = CreateUser();

        // Act
        user.MarkEmailAsConfirmed();

        // Assert
        user.IsEmailConfirmed.ShouldBeTrue();
    }
    
    [Fact]
    public void ChangeEmail_ValidEmail_ChangesEmail()
    {
        // Arrange
        var user = CreateUser();
        var newEmail = "new@example.com";

        // Act
        user.ChangeEmail(newEmail);

        // Assert
        user.Email.ShouldBe(new Email(newEmail));
    }
    
    [Fact]
    public void ChangeEmail_ValidEmail_ResetsEmailConfirmation()
    {
        // Arrange
        var user = CreateUser(isEmailConfirmed: true);
        var newEmail = "new@example.com";

        // Act
        user.ChangeEmail(newEmail);

        // Assert
        user.IsEmailConfirmed.ShouldBeFalse();
    }
    
    private static User CreateUser(
        DateTime? lastLoggedAtUtc = null,
        bool isEmailConfirmed = false)
    {
        return new User(
            new UserId(Guid.NewGuid()),
            new Email("user@example.com"),
            new Username("user"),
            new Password("password"),
            new Fullname("fullname example"),
            UserRole.User,
            DateTime.UtcNow,
            lastLoggedAtUtc,
            isEmailConfirmed);
    }
}