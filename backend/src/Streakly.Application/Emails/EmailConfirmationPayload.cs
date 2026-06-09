namespace Streakly.Application.Emails;

public record EmailConfirmationPayload(
    Guid UserId,
    string Email,
    EmailConfirmationPurpose Purpose);