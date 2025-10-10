using Identity.Domain.Abstractions;

namespace Identity.Domain.ValueObjects;

public class PasswordHash : ValueObject
{
    public string Value { get; }

    public PasswordHash(string hash)
    {
        if (string.IsNullOrWhiteSpace(hash))
            throw new DomainException("Хэш пароля не может быть пустым", "PASSWORD_HASH_EMPTY");

        Value = hash;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
