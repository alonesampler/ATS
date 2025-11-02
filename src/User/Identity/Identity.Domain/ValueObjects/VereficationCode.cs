using Identity.Domain.Abstractions;

namespace Identity.Domain.ValueObjects;

public class VereficationCode : ValueObject
{
    public string Code { get; private set; }

    public DateTime ExpiresAt { get; private set; }

    private VereficationCode() { }

    private VereficationCode(string code, DateTime expiresAt)
    {
        Code = code;
        ExpiresAt = expiresAt;
    }

    public static VereficationCode Create()
    {
        var random = new Random();
        var code = random.Next(100000, 999999).ToString();
        return new VereficationCode(code, DateTime.UtcNow.AddHours(1));
    }

    public bool IsExpired()
        => DateTime.UtcNow > ExpiresAt;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
        yield return ExpiresAt;
    }
}
