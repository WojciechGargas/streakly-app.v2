namespace Streakly.Core.Exceptions;

public class UsernameAlreadyInUseException(string username) : CustomException("$Username '{username}' is already in use")
{
}