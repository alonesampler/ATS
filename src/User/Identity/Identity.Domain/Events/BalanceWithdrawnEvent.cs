using Identity.Domain.Abstractions;

namespace Identity.Domain.Events;

public class BalanceWithdrawnEvent : DomainEvent
{
    public Guid UserId { get; }
    public decimal Amount { get; }
    public decimal NewBalance { get; }
    public DateTime WithdrawnAt { get; }

    public BalanceWithdrawnEvent(Guid userId, decimal amount, decimal newBalance)
    {
        UserId = userId;
        Amount = amount;
        NewBalance = newBalance;
        WithdrawnAt = DateTime.UtcNow;
    }
}
