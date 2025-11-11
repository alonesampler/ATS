namespace Identity.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }

    ISessionRepository SessionRepository { get; }

    Task SaveChangesAsync(bool applySoftDeleted = true);

    bool HasActiveTransaction { get; }

    Task BeginTransactionAsync();

    Task CommitTransactionAsync();

    Task RollbackTransactionAsync();
}

