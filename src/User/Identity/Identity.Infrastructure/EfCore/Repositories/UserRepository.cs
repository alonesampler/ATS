using Identity.Domain.Entities;
using Identity.Domain.Interfaces;
using Identity.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.EfCore.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IdentityDbContext _dbContext;

    public UserRepository(IdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(User user)
        => await _dbContext.Users.AddAsync(user);

    public async Task DeleteAsync(User user)
    {
        _dbContext.Users.Remove(user);
        await Task.CompletedTask;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.Value ==  email);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task UpdateAsync(User user)
    {
        _dbContext.Users.Update(user);
        await Task.CompletedTask;
    }
}
