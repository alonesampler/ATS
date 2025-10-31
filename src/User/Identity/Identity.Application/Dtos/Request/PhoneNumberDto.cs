namespace Identity.Application.Dtos.Create;

public class PhoneNumberDto
{
    /// <summary>
    /// Код страны телефонного номера
    /// </summary>
    public string CountryCode { get; init; }
    /// <summary>
    /// Телефонный номер без кода страны
    /// </summary>
    public string Number { get; init; }
}
