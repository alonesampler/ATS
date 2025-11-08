using Identity.Domain.Abstractions;
using Identity.Domain.ValueObjects;

namespace Identity.Domain.Events;

public class UserRegisteredEvent(Guid userId, string email, string firstName, string lastName, string? middleName, VereficationCode code) : DomainEvent
{
    public Guid UserId { get; } = userId;
    public string Email { get; } = email;
    public string FirstName { get; } = firstName;
    public string LastName { get; } = lastName;
    public string? MiddleName { get; } = middleName;
    public VereficationCode? EmailConfirmationCode { get; } = code;
}
