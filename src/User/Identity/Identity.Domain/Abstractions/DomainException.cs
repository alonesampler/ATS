namespace Identity.Domain.Abstractions;

public class DomainException : Exception
{
    /// <summary>
    /// Уникальный код ошибки.
    /// </summary>
    public string ErrorCode { get; }

    /// <summary>
    /// Дополнительные данные для обработки.
    /// </summary>
    public object? Details { get; }

    public DomainException(string message, string errorCode, object? details = null)
        : base(message)
    {
        ErrorCode = errorCode;
        Details = details;
    }

    public DomainException(string message)
        : base(message)
    {
        ErrorCode = "ERROR";
    }
}

