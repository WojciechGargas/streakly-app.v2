using Streakly.Core.Exceptions;

namespace Streakly.Application.Exceptions;

public class InvalidEmailConfirmationTokenException : CustomException
{
    public InvalidEmailConfirmationTokenException() : base("Email confirmation token is invalid or expired.")
    {
    }
}