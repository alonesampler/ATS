using Identity.Domain.Interfaces;
using Identity.Domain.ValueObjects;

namespace Identity.Infrastructure.Hasher;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }

    public bool VerifyPassword(string password, PasswordHash passwordHash)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash.Value);
    }
}
