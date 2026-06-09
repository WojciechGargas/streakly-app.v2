using Moq;
using Shouldly;
using Streakly.Application.Emails;
using Streakly.Application.Security;
using Streakly.Application.Users.Commands.SignUp;
using Streakly.Core.Abstractions;
using Streakly.Core.Entities;
using Streakly.Core.Enums;
using Streakly.Core.Exceptions;
using Streakly.Core.Repositories;
using Streakly.Core.ValueObjects.User;

namespace Streakly.Application.UnitTests.Users.SignUp;

public class SignUpHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPasswordManager> _passwordManagerMock;
    private readonly Mock<IEmailConfirmationService> _emailConfirmationServiceMock;
    private readonly Mock<IClock> _clockMock;
    private readonly SignUpHandler _handler;

    public SignUpHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _passwordManagerMock = new Mock<IPasswordManager>();
        _emailConfirmationServiceMock = new Mock<IEmailConfirmationService>();
        _clockMock = new Mock<IClock>();

        _handler = new SignUpHandler(
            _userRepositoryMock.Object,
            _passwordManagerMock.Object,
            _emailConfirmationServiceMock.Object,
            _clockMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_AddsUser()
    {
        // Arrange
        var command = CreateCommand();
        var securedPassword = "secured-password";
        var createdAtUtc = new DateTime(2026, 6, 9, 10, 0, 0, DateTimeKind.Utc);

        _userRepositoryMock
            .Setup(x => x.GetUserByEmailAsync(It.IsAny<Email>()))
            .ReturnsAsync((User?)null);

        _userRepositoryMock
            .Setup(x => x.GetUserByUserNameAsync(It.IsAny<string>()))
            .ReturnsAsync((User?)null);

        _passwordManagerMock
            .Setup(x => x.Secure(It.IsAny<string>()))
            .Returns(securedPassword);

        _clockMock
            .Setup(x => x.CurrentTimeUtc())
            .Returns(createdAtUtc);

        User? addedUser = null;

        _userRepositoryMock
            .Setup(x => x.AddUserAsync(It.IsAny<User>()))
            .Callback<User>(user => addedUser = user)
            .Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        addedUser.ShouldNotBeNull();
        addedUser.Email.ShouldBe(new Email(command.Email));
        addedUser.Username.ShouldBe(new Username(command.Username));
        addedUser.Password.ShouldBe(new Password(securedPassword));
        addedUser.Fullname.ShouldBe(new Fullname(command.FullName));
        addedUser.Role.ShouldBe(UserRole.User);
        addedUser.CreatedAt.ShouldBe(createdAtUtc);
        addedUser.LastLoggedAtUtc.ShouldBeNull();
        addedUser.IsEmailConfirmed.ShouldBeFalse();
    }

    [Fact]
    public async Task Handle_ValidCommand_SendsRegistrationConfirmation()
    {
        // Arrange
        var command = CreateCommand();
        var securedPassword = "secured-password";

        _userRepositoryMock
            .Setup(x => x.GetUserByEmailAsync(It.IsAny<Email>()))
            .ReturnsAsync((User?)null);

        _userRepositoryMock
            .Setup(x => x.GetUserByUserNameAsync(It.IsAny<string>()))
            .ReturnsAsync((User?)null);

        _passwordManagerMock
            .Setup(x => x.Secure(It.IsAny<string>()))
            .Returns(securedPassword);

        _clockMock
            .Setup(x => x.CurrentTimeUtc())
            .Returns(DateTime.UtcNow);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _emailConfirmationServiceMock.Verify(
            x => x.SendRegistrationConfirmationAsync(
                It.IsAny<Guid>(),
                It.Is<string>(email => email == command.Email)),
            Times.Once);
    }

    [Fact]
    public async Task Handle_EmailAlreadyExists_ThrowsEmailAlreadyInUseException()
    {
        // Arrange
        var command = CreateCommand();
        var existingUser = CreateUser(email: command.Email);

        _userRepositoryMock
            .Setup(x => x.GetUserByEmailAsync(It.IsAny<Email>()))
            .ReturnsAsync(existingUser);

        // Act
        var exception = await Should.ThrowAsync<EmailAlreadyInUseException>(() =>
            _handler.Handle(command, CancellationToken.None));

        // Assert
        exception.ShouldNotBeNull();

        _userRepositoryMock.Verify(
            x => x.AddUserAsync(It.IsAny<User>()),
            Times.Never);

        _emailConfirmationServiceMock.Verify(
            x => x.SendRegistrationConfirmationAsync(It.IsAny<Guid>(), It.IsAny<string>()),
            Times.Never);
    }

    [Fact]
    public async Task Handle_UsernameAlreadyExists_ThrowsUsernameAlreadyInUseException()
    {
        // Arrange
        var command = CreateCommand();
        var existingUser = CreateUser(username: command.Username);

        _userRepositoryMock
            .Setup(x => x.GetUserByEmailAsync(It.IsAny<Email>()))
            .ReturnsAsync((User?)null);

        _userRepositoryMock
            .Setup(x => x.GetUserByUserNameAsync(It.IsAny<string>()))
            .ReturnsAsync(existingUser);

        // Act
        var exception = await Should.ThrowAsync<UsernameAlreadyInUseException>(() =>
            _handler.Handle(command, CancellationToken.None));

        // Assert
        exception.ShouldNotBeNull();

        _userRepositoryMock.Verify(
            x => x.AddUserAsync(It.IsAny<User>()),
            Times.Never);

        _emailConfirmationServiceMock.Verify(
            x => x.SendRegistrationConfirmationAsync(It.IsAny<Guid>(), It.IsAny<string>()),
            Times.Never);
    }

    [Fact]
    public async Task Handle_ValidCommand_SecuresPassword()
    {
        // Arrange
        var command = CreateCommand();

        _userRepositoryMock
            .Setup(x => x.GetUserByEmailAsync(It.IsAny<Email>()))
            .ReturnsAsync((User?)null);

        _userRepositoryMock
            .Setup(x => x.GetUserByUserNameAsync(It.IsAny<string>()))
            .ReturnsAsync((User?)null);

        _passwordManagerMock
            .Setup(x => x.Secure(It.IsAny<string>()))
            .Returns("secured-password");

        _clockMock
            .Setup(x => x.CurrentTimeUtc())
            .Returns(DateTime.UtcNow);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _passwordManagerMock.Verify(
            x => x.Secure(It.Is<string>(password => password == command.Password)),
            Times.Once);
    }

    private static SignUpCommand CreateCommand(
        string email = "user@example.com",
        string username = "username",
        string password = "password",
        string fullName = "John Doe")
    {
        return new SignUpCommand(
            email,
            username,
            password,
            fullName);
    }

    private static User CreateUser(
        string email = "user@example.com",
        string username = "username")
    {
        return new User(
            new UserId(Guid.NewGuid()),
            new Email(email),
            new Username(username),
            new Password("secured-password"),
            new Fullname("John Doe"),
            UserRole.User,
            DateTime.UtcNow,
            null);
    }
}