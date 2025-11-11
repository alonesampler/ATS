using Identity.Domain.Abstractions;
using Identity.Domain.ValueObjects;

namespace Identity.Domain.Events;

public class UserLoginEvent(
    Guid userId,
    Guid sessionId,
    string email,
    string ipAddress,
    string deviceInfo,
    VereficationCode? verificationCode,
    bool isEmailConfirmed) : DomainEvent
{
    public Guid UserId { get; } = userId;
    public Guid SessionId { get; } = sessionId;
    public string Email { get; } = email;
    public VereficationCode? VerificationCode { get; } = verificationCode;
    public bool IsEmailConfirmed { get; } = isEmailConfirmed;
}
