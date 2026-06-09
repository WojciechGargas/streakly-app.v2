using MediatR;
using Streakly.Application.Emails;
using Streakly.Application.Exceptions;
using Streakly.Core.DomainServices.Interfaces;
using Streakly.Core.Exceptions;
using Streakly.Core.Repositories;
using Streakly.Core.ValueObjects;

namespace Streakly.Application.Users.Commands.ConfirmEmail;

internal sealed class ConfirmEmailHandler(
    IUserRepository userRepository,
    IEmailConfirmationService emailConfirmationService,
    IUserService userService) : IRequestHandler<ConfirmEmailCommand>
{
    public async Task Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var payload = emailConfirmationService.ReadToken(request.Token);
        
        var user = await userRepository.GetUserByIdAsync(payload.UserId) ??
                   throw new UserNotFoundException(payload.UserId);

        if (payload.Purpose == EmailConfirmationPurpose.Registration)
        {
            if (user.Email != payload.Email)
                throw new InvalidEmailConfirmationTokenException();
            
            await userService.MarkEmailAsConfirmed(user, user);
            return;
        }
        
        var newEmail = new Email(payload.Email);
        var existingUser = await userRepository.GetUserByEmailAsync(newEmail);
        
        if(existingUser is not null && existingUser.UserId != user.UserId)
            throw new EmailAlreadyInUseException(newEmail);
        
        await userService.ChangeEmail(user, user, newEmail);
    }
}