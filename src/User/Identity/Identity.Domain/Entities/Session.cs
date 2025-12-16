using Identity.Domain.Abstractions;

namespace Identity.Domain.Entities;

public class Session : Entity<Guid>
{
    private Session() : base() { }

    private Session(
        Guid id,
        Guid userId,
        DateTime expiresAt,
        string refreshTokenHash) : base(id)
    {
        UserId = userId;
    }

    public Guid UserId { get; }
    
    public DateTime ExpiresAt { get; private set; }
    
    public string RefreshTokenHash { get; private set; }
    
    public bool IsRevoked { get; private set; }
    
    public static Session Create(
        Guid id,
        Guid userId,
        DateTime expiresAt,
        string refreshTokenHash)
        => new Session(id, userId, expiresAt, refreshTokenHash);

    public void Revoke() => IsRevoked = true;
}