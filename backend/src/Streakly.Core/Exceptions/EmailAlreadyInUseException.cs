namespace Streakly.Core.Exceptions;

public class EmailAlreadyInUseException(string email) : CustomException($"Email '{email}' is already in use")
{
}