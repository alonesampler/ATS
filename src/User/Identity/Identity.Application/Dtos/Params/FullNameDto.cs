namespace Identity.Application.Dtos.Params;

public class FullNameDto
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string FirstName { get; init; }

    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string LastName { get; init; }

    /// <summary>
    /// Отчество пользователя
    /// </summary>
    public string? MiddleName { get; init; }
}
