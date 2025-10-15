namespace Identity.Application.Dtos.Create;

public class CreateFullNameDto
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
