using Identity.Domain.Abstractions;
using Identity.Domain.ValueObjects;

namespace Identity.Domain.Entities;

public class User : Entity<Guid>
{
    private readonly List<Session> _sessions = new();
    public IReadOnlyCollection<Session> Sessions => _sessions.AsReadOnly();

    private User() : base() { }

    private User(
        Guid id,
        Email email,
        PhoneNumber? phoneNumber,
        PasswordHash passwordHash,
        DateTime registeredAt,
        bool isEmailConfirmed,
        VereficationCode? confirmationCode)
        : base(id)
    {
        Email = email;
        PasswordHash = passwordHash;
        PhoneNumber = phoneNumber;
        RegisteredAt = registeredAt;
        IsEmailConfirmed = isEmailConfirmed;
        EmailConfirmationCode = confirmationCode;
    }

    public Email Email { get; private set; }
    public PhoneNumber? PhoneNumber { get; private set; }
    public PasswordHash PasswordHash { get; private set; }
    public DateTime RegisteredAt { get; private set; }
    public bool IsEmailConfirmed { get; private set; }
    public VereficationCode? EmailConfirmationCode { get; private set; }

    public static User Register(
        Guid id,
        Email email,
        PhoneNumber? phoneNumber,
        PasswordHash passwordHash,
        DateTime registeredAt,
        bool isEmailConfirmed,
        VereficationCode? confirmationCode)
    {
        if (Guid.Empty == id)
            throw new DomainException("Идентификатор пользователя не может быть пустым", "USER_ID_EMPTY");

        return new User(id, email, phoneNumber, passwordHash, registeredAt, isEmailConfirmed, confirmationCode);
    }

    public void ConfirmEmail(string confirmationCode)
    {
        if (IsEmailConfirmed)
            throw new DomainException("Email уже подтвержден", "EMAIL_ALREADY_CONFIRMED");

        if (EmailConfirmationCode is null || EmailConfirmationCode.Code != confirmationCode || EmailConfirmationCode.IsExpired())
            throw new DomainException("Неверный код подтверждения", "INVALID_CONFIRMATION_CODE");

        EmailConfirmationCode = null;
        IsEmailConfirmed = true;
    }

    public void ChangePassword(PasswordHash newPasswordHash)
    {
        if (newPasswordHash is null)
            throw new DomainException("Новый пароль не может быть пустым", "PASSWORD_EMPTY");

        PasswordHash = newPasswordHash;
    }

    public void UpdatePhoneNumber(PhoneNumber phoneNumber)
    {
        PhoneNumber = phoneNumber;
    }

    public void AddSession(Session session)
        => _sessions.Add(session);

    public void RemoveSession(Guid sessionId)
    {
        var session = _sessions.FirstOrDefault(s => s.Id == sessionId);

        if (session != null)
            _sessions.Remove(session);
    }
}