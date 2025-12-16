namespace Identity.Application.Interfaces.Services;

public interface IRefreshTokenService
{
    string Generate();
    string Hash(string token);
    bool Verify(string token, string hash);
}
