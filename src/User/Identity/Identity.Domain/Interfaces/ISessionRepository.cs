using Identity.Domain.Entities;

namespace Identity.Domain.Interfaces;

public interface ISessionRepository
{
    Task<Session?> GetByIdAsync(Guid sessionId);
    Task AddAsync(Session session);
    Task RemoveAsync(Guid sessionId);
}