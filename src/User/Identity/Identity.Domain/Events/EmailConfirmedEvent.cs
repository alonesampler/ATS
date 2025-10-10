using Identity.Domain.Abstractions;

namespace Identity.Domain.Events;

public class EmailConfirmedEvent : DomainEvent
{
    public Guid UserId { get; }
    public DateTime ConfirmedAt { get; }

    public EmailConfirmedEvent(Guid userId)
    {
        UserId = userId;
        ConfirmedAt = DateTime.UtcNow;
    }
}
