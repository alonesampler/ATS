using Identity.Domain.Interfaces;
using Identity.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Identity.Infrastructure.EfCore.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly IdentityDbContext _dbContext;
    private IDbContextTransaction _currentTransaction;

    public UnitOfWork(IdentityDbContext dbContext, IUserRepository userRepository)
    {
        _dbContext = dbContext;
        UserRepository = userRepository;
    }

    public IUserRepository UserRepository { get; }

    public void Dispose()
        => _dbContext.Dispose();

    public Task SaveChangesAsync(bool applySoftDeleted = true)
        => _dbContext.SaveChangesAsync(applySoftDeleted);

    public bool HasActiveTransaction
        => _currentTransaction is not null;


    public async Task BeginTransactionAsync()
    {
        if (_currentTransaction is not null)
        {
            throw new InvalidOperationException("A transaction is already in progress.");
        }

        _currentTransaction = await _dbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await _dbContext.SaveChangesAsync();

            await _currentTransaction?.CommitAsync();
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            if (_currentTransaction is not null)
            {
                await _currentTransaction.DisposeAsync(); // будет закрыта сама транзакция.
                _currentTransaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_currentTransaction is null)
        {
            throw new InvalidOperationException("A transaction must be in progress to execute rollback.");
        }
        try
        {
            await _currentTransaction.RollbackAsync();
        }
        finally
        {
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }

}
