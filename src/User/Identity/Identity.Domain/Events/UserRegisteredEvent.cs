using Identity.Domain.Abstractions;
using Identity.Domain.ValueObjects;

namespace Identity.Domain.Events;

public class UserRegisteredEvent : DomainEvent
{
    public Guid UserId { get; }
    public string Email { get; }
    public string FirstName { get; }
    public string LastName { get; }

    public UserRegisteredEvent(Guid userId, Email email, string firstName, string lastName)
    {
        UserId = userId;
        Email = email.Value;
        FirstName = firstName;
        LastName = lastName;
    }
}
