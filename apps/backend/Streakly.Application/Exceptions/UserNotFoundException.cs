using Streakly.Core.Exceptions;

namespace Streakly.Application.Exceptions;

public class UserNotFoundException(Guid userId) : CustomException($"User with ID : '{userId}' was not found.")
{
    public Guid UserId { get; private set; } = userId;
}