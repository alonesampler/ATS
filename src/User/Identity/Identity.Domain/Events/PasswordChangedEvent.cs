using Identity.Domain.Abstractions;

namespace Identity.Domain.Events;

public class PasswordChangedEvent : DomainEvent
{
    public Guid UserId { get; }
    public DateTime ChangedAt { get; }

    public PasswordChangedEvent(Guid userId)
    {
        UserId = userId;
        ChangedAt = DateTime.UtcNow;
    }
}
