namespace Identity.Domain.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    public Task AddAsync(TEntity entity);
    public Task UpdateAsync(TEntity entity);
    public Task DeleteAsync(TEntity entity);
}
