using Identity.Domain.Abstractions;

namespace Identity.Domain.Events;

public class EmailConfirmedEvent(Guid userId, string email, string firstName, string lastname, string? phone) : DomainEvent
{
    public Guid UserId { get; } = userId;

    public string Email { get; } = email;

    public string FirstName { get; } = firstName;

    public string LastName { get; } = lastname;

    public string? Phone { get; } = phone;
}
