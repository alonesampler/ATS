using Identity.Domain.Abstractions;

namespace Identity.Domain.ValueObjects;

public class PhoneNumber : ValueObject
{
    public string CountryCode { get; }
    public string Number { get; }
    public string FullNumber => $"+{CountryCode}{Number}";

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

    public PhoneNumber(string fullNumber)
    {
        if (string.IsNullOrWhiteSpace(fullNumber))
            throw new DomainException("Номер телефона не может быть пустым", "INVALID_PHONE_NUMBER");

        var cleanNumber = fullNumber.StartsWith('+')
            ? fullNumber[1..]
            : fullNumber;

        cleanNumber = new string(cleanNumber.Where(char.IsDigit).ToArray());

        if (cleanNumber.Length < 8 || cleanNumber.Length > 16)
            throw new DomainException("Некорректная длина номера телефона", "INVALID_PHONE_LENGTH");

        var possibleCountryCodes = new[] { 3, 2, 1 };
        string countryCode = "";
        string number = "";

        foreach (var length in possibleCountryCodes)
        {
            if (cleanNumber.Length >= length + 7)
            {
                countryCode = cleanNumber[..length];
                number = cleanNumber[length..];
                break;
            }
        }

        if (string.IsNullOrEmpty(countryCode))
        {
            countryCode = "7";
            number = cleanNumber;
        }

        CountryCode = countryCode;
        Number = number;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return CountryCode;
        yield return Number;
    }

    public override string ToString() => FullNumber;
    public static PhoneNumber CreateRussian(string number) => new("7", number);
    public static PhoneNumber CreateUkrainian(string number) => new("380", number);
    public static PhoneNumber CreateKazakh(string number) => new("7", number);

    public bool IsRussianNumber() => CountryCode == "7" && Number.Length == 10;

    public string ToFormattedString()
    {
        return CountryCode switch
        {
            "7" => FormatRussianNumber(),
            "380" => FormatUkrainianNumber(),
            "375" => FormatBelarusianNumber(),
            _ => FullNumber
        };
    }

    private string FormatRussianNumber()
    {
        if (Number.Length != 10) return FullNumber;

        return $"+7 ({Number[0..3]}) {Number[3..6]}-{Number[6..8]}-{Number[8..10]}";
    }

    private string FormatUkrainianNumber()
    {
        if (Number.Length != 9) return FullNumber;

        return $"+380 ({Number[0..2]}) {Number[2..5]}-{Number[5..7]}-{Number[7..9]}";
    }

    private string FormatBelarusianNumber()
    {
        if (Number.Length != 9) return FullNumber;

        return $"+375 ({Number[0..2]}) {Number[2..5]}-{Number[5..7]}-{Number[7..9]}";
    }
    public string ToMaskedString()
    {
        if (Number.Length < 4) return FullNumber;

        var visiblePart = Number[^4..];
        var maskedPart = new string('*', Number.Length - 4);
        return $"+{CountryCode}{maskedPart}{visiblePart}";
    }
}
