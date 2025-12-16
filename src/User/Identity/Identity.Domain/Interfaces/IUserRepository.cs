using Identity.Domain.Entities;

namespace Identity.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);

    Task<User?> GetByEmailAsync(string email);
    
    Task<User?> GetByPhoneAsync(string phone);

    Task AddAsync(User user);

    Task UpdateAsync(User user);

    Task DeleteAsync(User user);
}
