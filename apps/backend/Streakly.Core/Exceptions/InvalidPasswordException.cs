namespace Streakly.Core.Exceptions;

public class InvalidPasswordException(string password) : CustomException($"Provided password: {password} is not a valid password.")
{
    public string Password { get; } = password;
}