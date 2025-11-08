using Identity.Domain.Abstractions;

namespace Identity.Domain.Events;

public class EmailConfirmedEvent(Guid userId, string email) : DomainEvent
{
    public Guid UserId { get; } = userId;

    public string Email { get; } = email;
}
