using Identity.Domain.Abstractions;

namespace Identity.Domain.Entities;

public class Session : Entity<Guid>
{
    private Session() : base() { }

    private Session(Guid id, Guid userId, DateTime expiresAt, string deviceInfo, string ipAddress) : base(id)
    {
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
        ExpiresAt = expiresAt;
        DeviceInfo = deviceInfo;
        IpAddress = ipAddress;
    }

    public Guid UserId { get; private set; }
    public User User { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public string DeviceInfo { get; private set; }
    public string IpAddress { get; private set; }

    public static Session Create(Guid id, Guid userId, DateTime expiresAt, string deviceInfo, string ipAddress)
        => new Session(id, userId, expiresAt, deviceInfo, ipAddress);

    public bool IsExpired() => DateTime.UtcNow > ExpiresAt;
}