using Identity.Domain.Entities;

namespace Identity.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);

    Task CreateAsync(User user);

    Task UpdateAsync(User user);

    Task DeleteAsync(User user);
}
