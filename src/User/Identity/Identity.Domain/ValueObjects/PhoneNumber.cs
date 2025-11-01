using Identity.Domain.Abstractions;

namespace Identity.Domain.ValueObjects;

public class PhoneNumber : ValueObject
{
    public string CountryCode { get; }
    public string Number { get; }

    public PhoneNumber(string countryCode, string number)
    {
        if (string.IsNullOrWhiteSpace(countryCode))
            throw new DomainException("Код страны не может быть пустым", "INVALID_COUNTRY_CODE");

        if (string.IsNullOrWhiteSpace(number))
            throw new DomainException("Номер телефона не может быть пустым", "INVALID_PHONE_NUMBER");

        if (!countryCode.All(char.IsDigit) || countryCode.Length < 1 || countryCode.Length > 3)
            throw new DomainException("Некорректный код страны", "INVALID_COUNTRY_CODE_FORMAT");

        var cleanNumber = new string(number.Where(char.IsDigit).ToArray());
        if (cleanNumber.Length < 7 || cleanNumber.Length > 15)
            throw new DomainException("Некорректная длина номера телефона", "INVALID_PHONE_LENGTH");

        CountryCode = countryCode;
        Number = cleanNumber;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return CountryCode;
        yield return Number;
    }
}
