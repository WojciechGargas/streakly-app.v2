namespace Streakly.Core.Exceptions;

public class InvalidFrequencyException() : CustomException("Frequency must contain at least one day.")
{
}