namespace Streakly.Core.Exceptions;

public class InvalidEntityException(object id) : CustomException($"Cannot set: {id} as entity identifier.")
{
    public object Id { get; } = id;
}