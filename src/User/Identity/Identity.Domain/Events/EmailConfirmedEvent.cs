using Identity.Domain.Abstractions;

namespace Identity.Domain.Events;

public class EmailConfirmedEvent : DomainEvent
{
    public Guid UserId { get; init; }

    public EmailConfirmedEvent(Guid userId)
    {
        UserId = userId;
    }
}
