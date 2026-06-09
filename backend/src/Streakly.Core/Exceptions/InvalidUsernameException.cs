namespace Streakly.Core.Exceptions;

public class InvalidUsernameException(string username) : CustomException($"Username: {username} is not a valid username.")
{
    public string Username { get; } = username;
}