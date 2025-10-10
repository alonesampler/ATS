using Identity.Domain.Abstractions;
using Identity.Domain.Enums;

namespace Identity.Domain.Events;

public class UserStatusChangedEvent : DomainEvent
{
    public Guid UserId { get; }
    public UserStatus OldStatus { get; }
    public UserStatus NewStatus { get; }
    public DateTime ChangedAt { get; }

    public UserStatusChangedEvent(Guid userId, UserStatus oldStatus, UserStatus newStatus)
    {
        UserId = userId;
        OldStatus = oldStatus;
        NewStatus = newStatus;
        ChangedAt = DateTime.UtcNow;
    }
}
