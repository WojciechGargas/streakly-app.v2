using MediatR;

namespace Streakly.Application.Users.Commands.Logout;

public record LogoutCommand(Guid UserId, string TokenId, DateTime ExpiresAtUtc) : IRequest;
