using Identity.Domain.Abstractions;

namespace Identity.Domain.ValueObjects;

public class FullName : ValueObject
{
    public string FirstName { get; }

    public string LastName { get; }

    public string? MiddleName { get; }

    public FullName(string firstName, string lastName, string? middleName = null)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new DomainException("Имя не может быть пустым.", nameof(firstName));
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new DomainException("Фамилия не может быть пустой", nameof(lastName));
        }

        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
        if (MiddleName is not null)
            yield return MiddleName;
    }
}
