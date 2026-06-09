using Streakly.Core.Exceptions;

namespace Streakly.Core.ValueObjects.User;

public sealed record Fullname
{
    public string Value { get;}

    public Fullname(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length is < 1 or > 32)
        {
            throw new InvalidFullnameException(value);
        }
        
        Value = value;
    }
    
    public static implicit operator Fullname(string value) => new Fullname(value);
    
    public static implicit operator string(Fullname value) => value.Value;
    
    public override string ToString() => Value;
}