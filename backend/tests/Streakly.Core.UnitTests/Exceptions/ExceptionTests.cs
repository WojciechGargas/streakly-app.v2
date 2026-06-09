using Shouldly;
using Streakly.Core.Exceptions;

namespace Streakly.Core.UnitTests.Exceptions;

public class ExceptionTests
{
    [Fact]
    public void CustomException_WithMessage_SetsMessage()
    {
        // Arrange
        var message = "Custom error.";

        // Act
        var exception = new CustomException(message);

        // Assert
        exception.Message.ShouldBe(message);
    }

    [Fact]
    public void EmailAlreadyInUseException_Ctor_SetsMessage()
    {
        // Act
        var exception = new EmailAlreadyInUseException("user@example.com");

        // Assert
        exception.Message.ShouldBe("$Email '{email}' is already in use");
    }

    [Fact]
    public void InvalidDateException_Ctor_SetsMessage()
    {
        // Act
        var exception = new InvalidDateException();

        // Assert
        exception.Message.ShouldBe("Invalid date.");
    }

    [Fact]
    public void InvalidEmailException_Ctor_SetsMessageAndEmail()
    {
        // Arrange
        var email = "invalid-email";

        // Act
        var exception = new InvalidEmailException(email);

        // Assert
        exception.Message.ShouldBe("Email: invalid-email is not a valid email address.");
        exception.Email.ShouldBe(email);
    }

    [Fact]
    public void InvalidEntityException_Ctor_SetsMessageAndId()
    {
        // Arrange
        var id = Guid.Empty;

        // Act
        var exception = new InvalidEntityException(id);

        // Assert
        exception.Message.ShouldBe("Cannot set: 00000000-0000-0000-0000-000000000000 as entity identifier.");
        exception.Id.ShouldBe(id);
    }

    [Fact]
    public void InvalidFrequencyException_Ctor_SetsMessage()
    {
        // Act
        var exception = new InvalidFrequencyException();

        // Assert
        exception.Message.ShouldBe("Frequency must contain at least one day.");
    }

    [Fact]
    public void InvalidFullnameException_Ctor_SetsMessageAndFullname()
    {
        // Arrange
        var fullname = string.Empty;

        // Act
        var exception = new InvalidFullnameException(fullname);

        // Assert
        exception.Message.ShouldBe("Full name:  is not a valid full name.");
        exception.Fullname.ShouldBe(fullname);
    }

    [Fact]
    public void InvalidGoalProgressionException_Ctor_SetsMessage()
    {
        // Arrange
        var message = "Invalid progression.";

        // Act
        var exception = new InvalidGoalProgressionException(message);

        // Assert
        exception.Message.ShouldBe(message);
    }

    [Fact]
    public void InvalidHabitNameException_Ctor_SetsMessageAndHabitName()
    {
        // Arrange
        var habitName = string.Empty;

        // Act
        var exception = new InvalidHabitNameException(habitName);

        // Assert
        exception.Message.ShouldBe("Habit name:  is not a valid habit name.");
        exception.HabitName.ShouldBe(habitName);
    }

    [Fact]
    public void InvalidInitialTargetException_Ctor_SetsMessage()
    {
        // Act
        var exception = new InvalidInitialTargetException();

        // Assert
        exception.Message.ShouldBe("Initial target must be greater than zero.");
    }

    [Fact]
    public void InvalidPasswordException_Ctor_SetsMessageAndPassword()
    {
        // Arrange
        var password = "123";

        // Act
        var exception = new InvalidPasswordException(password);

        // Assert
        exception.Message.ShouldBe("Provided password: 123 is not a valid password.");
        exception.Password.ShouldBe(password);
    }

    [Fact]
    public void InvalidUsernameException_Ctor_SetsMessageAndUsername()
    {
        // Arrange
        var username = "ab";

        // Act
        var exception = new InvalidUsernameException(username);

        // Assert
        exception.Message.ShouldBe("Username: ab is not a valid username.");
        exception.Username.ShouldBe(username);
    }

    [Fact]
    public void UserAccessDeniedException_Ctor_SetsMessage()
    {
        // Act
        var exception = new UserAccessDeniedException();

        // Assert
        exception.Message.ShouldBe("You are not allowed to update this user.");
    }

    [Fact]
    public void UsernameAlreadyInUseException_Ctor_SetsMessage()
    {
        // Act
        var exception = new UsernameAlreadyInUseException("user");

        // Assert
        exception.Message.ShouldBe("$Username '{username}' is already in use");
    }
}
