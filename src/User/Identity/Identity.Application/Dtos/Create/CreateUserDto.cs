namespace Identity.Application.Dtos.Create;

public class CreateUserDto
{
    /// <summary>
    /// Почтовый адрес пользователя
    /// </summary>
    public string Email { get; init; }
    /// <summary>
    /// Телефонный номер пользователя
    /// </summary>
    public string? PhoneNumber { get; init; }
    /// <summary>
    /// ФИО пользователя
    /// </summary>
    public CreateFullNameDto FullName { get; init; }
    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public string Password { get; init; }
}
