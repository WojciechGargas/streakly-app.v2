
namespace Streakly.Application.Users.Commands.SignUp;

public record SignUpRequest(
    string Email,
    string UserName,
    string Password,
    string FullName);
