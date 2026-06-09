namespace Streakly.Infrastructure.DAL.Entities;

public sealed class RevokedToken
{
    public RevokedToken(string tokenId, Guid userId, DateTime expiresAtUtc,  DateTime revokedAtUtc)
    {
        Id = Guid.NewGuid();
        TokenId = tokenId;
        UserId = userId;
        ExpiresAtUtc = expiresAtUtc;
        RevokedAtUtc = revokedAtUtc;
    }

    public Guid Id { get; private set; }
    public string TokenId { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime ExpiresAtUtc { get; private set; }
    public DateTime RevokedAtUtc { get; private set; }

    private RevokedToken()
    {
    }
    
}