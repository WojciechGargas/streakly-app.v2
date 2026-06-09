using MediatR;

namespace Streakly.Application.Users.Commands.ConfirmEmail;

public record ConfirmEmailCommand(string Token) : IRequest;