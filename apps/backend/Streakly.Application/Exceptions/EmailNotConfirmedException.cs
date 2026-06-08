using Streakly.Core.Exceptions;

namespace Streakly.Application.Exceptions;

public class EmailNotConfirmedException : CustomException
{
    public EmailNotConfirmedException() : base("Your email address is not confirmed")
    {
    }
}