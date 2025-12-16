using System.Security.Cryptography;
using System.Text;
using Identity.Application.Interfaces.Services;

namespace Identity.Infrastructure.Security;

public class RefreshTokenService : IRefreshTokenService
{
    public string Generate()
    {
        var bytes = RandomNumberGenerator.GetBytes(64);
        
        return Convert.ToBase64String(bytes);
    }

    public string Hash(string token)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(token);
        
        return Convert.ToBase64String(sha256.ComputeHash(bytes));
    }

    public bool Verify(string token, string hash)
    {
        var tokenHash = Hash(token);
        
        return CryptographicOperations.FixedTimeEquals(
            Convert.FromBase64String(tokenHash),
            Convert.FromBase64String(hash)
        );
    }
}