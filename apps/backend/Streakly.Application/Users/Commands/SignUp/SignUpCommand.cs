using MediatR;

namespace Streakly.Application.Users.Commands.SignUp;

public record SignUpCommand(
    string Email,
    string Username,
    string Password,
    string FullName) : IRequest;