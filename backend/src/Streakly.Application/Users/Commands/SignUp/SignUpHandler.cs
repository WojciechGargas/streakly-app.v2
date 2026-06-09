using MediatR;
using Streakly.Application.Emails;
using Streakly.Application.Security;
using Streakly.Core.Abstractions;
using Streakly.Core.Entities;
using Streakly.Core.Enums;
using Streakly.Core.Exceptions;
using Streakly.Core.Repositories;
using Streakly.Core.ValueObjects;
using Streakly.Core.ValueObjects.User;


namespace Streakly.Application.Users.Commands.SignUp;

public class SignUpHandler(
    IUserRepository userRepository,
    IPasswordManager passwordManager,
    IEmailConfirmationService emailConfirmationService,
    IClock clock)
    : IRequestHandler<SignUpCommand>
{
    public async Task Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var userId = new UserId(Guid.NewGuid());
        var email = new Email(request.Email);
        var username = new Username(request.Username);
        var password = new Password(request.Password);
        var fullname = new Fullname(request.FullName);
        var role = UserRole.User;

        if (await userRepository.GetUserByEmailAsync(email) is not null)
        {
            throw new EmailAlreadyInUseException(email);
        }

        if (await userRepository.GetUserByUserNameAsync(username) is not null)
        {
            throw new UsernameAlreadyInUseException(username);
        }

        var securedPassword = passwordManager.Secure(password);
        var user = new User(userId, email, username, securedPassword,
            fullname, role, clock.CurrentTimeUtc(), null);

        await userRepository.AddUserAsync(user);
        await emailConfirmationService.SendRegistrationConfirmationAsync(user.UserId, user.Email);
    }
}
