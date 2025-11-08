namespace Identity.Application.Dtos.Params;

public class UserDto
{
    /// <summary>
    /// Почтовый адрес пользователя
    /// </summary>
    public string Email { get; init; }
    /// <summary>
    /// Телефонный номер пользователя
    /// </summary>
    public PhoneNumberDto? PhoneNumber { get; init; }
    /// <summary>
    /// ФИО пользователя
    /// </summary>
    public FullNameDto FullName { get; init; }
    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public string Password { get; init; }
}
