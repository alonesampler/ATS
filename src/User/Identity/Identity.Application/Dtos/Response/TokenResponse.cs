namespace Identity.Application.Dtos.Response;

public record TokenResponse(
    string AccessToken,
    string RefreshToken,
    int ExpiresIn
);