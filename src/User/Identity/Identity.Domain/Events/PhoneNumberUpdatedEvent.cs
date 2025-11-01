using Identity.Domain.Abstractions;
using Identity.Domain.ValueObjects;

namespace Identity.Domain.Events;

public class PhoneNumberUpdatedEvent : DomainEvent
{
    public Guid UserId { get; }
    public string PhoneNumber { get; }

    public PhoneNumberUpdatedEvent(Guid userId, PhoneNumber phoneNumber)
    {
        UserId = userId;
        PhoneNumber = $"{phoneNumber.CountryCode}{phoneNumber.Number}";
    }
}
