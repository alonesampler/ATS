using Identity.Domain.Abstractions;
using Identity.Domain.Events;
using Identity.Domain.ValueObjects;

namespace Identity.Domain.Entities;

public class User : AggregateRoot<Guid>
{
    private User() : base() { }

    private User(
        Guid id,
        Email email,
        PhoneNumber? phoneNumber,
        FullName fullName,
        PasswordHash passwordHash,
        DateTime registeredAt,
        bool isEmailConfirmed)
        : base(id)
    {
        Email = email;
        PasswordHash = passwordHash;
        PhoneNumber = phoneNumber;
        FullName = fullName;
        RegisteredAt = registeredAt;
        IsEmailConfirmed = isEmailConfirmed;

        AddDomainEvent(new UserRegisteredEvent(Id, Email, FullName.FirstName, FullName.LastName));
    }

    public Email Email { get; private set; }
    public PhoneNumber? PhoneNumber { get; private set; }
    public PasswordHash PasswordHash { get; private set; }
    public FullName FullName { get; private set; }
    public DateTime RegisteredAt { get; private set; }
    public bool IsEmailConfirmed { get; private set; }

    public static User Register(
        Guid id,
        Email email,
        PhoneNumber phoneNumber,
        FullName fullName,
        PasswordHash passwordHash,
        DateTime registeredAt,
        bool isEmailConfirmed)
    {
        if (Guid.Empty == id)
            throw new DomainException("Идентификатор пользователя не может быть пустым", "USER_ID_EMPTY");

        return new User(id, email, phoneNumber, fullName, passwordHash, registeredAt, isEmailConfirmed);
    }

    public void ConfirmEmail()
    {
        if (IsEmailConfirmed)
            throw new DomainException("Email уже подтвержден", "EMAIL_ALREADY_CONFIRMED");

        IsEmailConfirmed = true;
        AddDomainEvent(new EmailConfirmedEvent(Id));
    }

    public void ChangePassword(PasswordHash newPasswordHash)
    {
        if (newPasswordHash is null)
            throw new DomainException("Новый пароль не может быть пустым", "PASSWORD_EMPTY");

        PasswordHash = newPasswordHash;
        AddDomainEvent(new PasswordChangedEvent(Id));
    }

    public void UpdatePhoneNumber(PhoneNumber phoneNumber)
    {
        PhoneNumber = phoneNumber;
        AddDomainEvent(new PhoneNumberUpdatedEvent(Id, phoneNumber));
    }
}
