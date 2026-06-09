namespace Streakly.Core.Exceptions;

public class InvalidFullnameException(string fullname) : CustomException($"Full name: {fullname} is not a valid full name.")
{
    public string Fullname { get; } =  fullname;
}