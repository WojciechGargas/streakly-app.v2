using MediatR;
using Streakly.Application.Security;

namespace Streakly.Application.Users.Commands.Logout;

public class LogoutHandler(
    ITokenRevocationService tokenRevocationService) 
    : IRequestHandler<LogoutCommand>
{
    public Task Handle(LogoutCommand request, CancellationToken cancellationToken)
        => tokenRevocationService.RevokeTokenAsync(
            request.TokenId,
            request.UserId,
            request.ExpiresAtUtc);
}