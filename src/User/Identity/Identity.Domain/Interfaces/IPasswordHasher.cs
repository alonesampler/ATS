using Identity.Domain.ValueObjects;

namespace Identity.Domain.Interfaces;

public interface IPasswordHasher
{
    PasswordHash HashPassword(string password);
    bool VerifyPassword(string password, PasswordHash passwordHash);
}
