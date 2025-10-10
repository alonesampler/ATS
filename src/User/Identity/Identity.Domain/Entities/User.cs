using Identity.Domain.Abstractions;
using Identity.Domain.Events;
using Identity.Domain.ValueObjects;

namespace Identity.Domain.Entities;

public class User : AggregateRoot<Guid>
{
    private User() : base() { }

    private User(Guid id, Email email, string firstName, string lastName, PasswordHash passwordHash)
        : base(id)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        PasswordHash = passwordHash;
        RegisteredAt = DateTime.UtcNow;
        IsEmailConfirmed = false;
        Balance = 0;

        AddDomainEvent(new UserRegisteredEvent(Id, Email, FirstName, LastName));
    }

    public Email Email { get; private set; }
    public PhoneNumber? PhoneNumber { get; private set; }
    public PasswordHash PasswordHash { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime RegisteredAt { get; private set; }
    public bool IsEmailConfirmed { get; private set; }
    public decimal Balance { get; private set; }

    public static User Register(Guid id, Email email, string firstName, string lastName, PasswordHash passwordHash)
    {
        if (Guid.Empty == id)
            throw new DomainException("Идентификатор пользователя не может быть пустым", "USER_ID_EMPTY");

        if (string.IsNullOrWhiteSpace(firstName))
            throw new DomainException("Имя не может быть пустым", "FIRST_NAME_EMPTY");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new DomainException("Фамилия не может быть пустой", "LAST_NAME_EMPTY");

        return new User(id, email, firstName, lastName, passwordHash);
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

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new DomainException("Сумма пополнения должна быть положительной", "INVALID_DEPOSIT_AMOUNT");

        Balance += amount;
        AddDomainEvent(new BalanceDepositedEvent(Id, amount, Balance));
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new DomainException("Сумма списания должна быть положительной", "INVALID_WITHDRAW_AMOUNT");

        if (Balance < amount)
            throw new DomainException("Недостаточно средств", "INSUFFICIENT_BALANCE");

        Balance -= amount;
        AddDomainEvent(new BalanceWithdrawnEvent(Id, amount, Balance));
    }
}
