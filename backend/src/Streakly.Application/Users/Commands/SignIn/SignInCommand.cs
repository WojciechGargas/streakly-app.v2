using MediatR;
using Streakly.Application.DTO;

namespace Streakly.Application.Users.Commands.SignIn;

public record SignInCommand(string Email, string Password) : IRequest<JwtDto>;