namespace Streakly.Core.Exceptions;

public class InvalidEmailException(string email) : CustomException($"Email: {email} is not a valid email address.")
{
    public string Email { get; } = email;
}