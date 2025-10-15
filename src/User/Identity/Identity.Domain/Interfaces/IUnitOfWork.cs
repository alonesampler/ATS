namespace Identity.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }

    Task SaveChangesAsync(bool applySoftDeleted = true);

    bool HasActiveTransaction { get; }

    Task BeginTransactionAsync();

    Task CommitTransactionAsync();

    Task RollbackTransactionAsync();
}

