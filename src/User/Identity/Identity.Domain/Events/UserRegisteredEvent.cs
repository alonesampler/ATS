using Identity.Domain.Abstractions;

namespace Identity.Domain.Events;

public class UserRegisteredEvent(Guid userId, string email, string firstName, string lastName) : DomainEvent
{
    public Guid UserId { get; } = userId;
    public string Email { get; } = email;
    public string FirstName { get; } = firstName;
    public string LastName { get; } = lastName;
}
