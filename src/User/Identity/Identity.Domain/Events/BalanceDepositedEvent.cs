using Identity.Domain.Abstractions;

namespace Identity.Domain.Events;

public class BalanceDepositedEvent : DomainEvent
{
    public Guid UserId { get; }
    public decimal Amount { get; }
    public decimal NewBalance { get; }
    public DateTime DepositedAt { get; }

    public BalanceDepositedEvent(Guid userId, decimal amount, decimal newBalance)
    {
        UserId = userId;
        Amount = amount;
        NewBalance = newBalance;
        DepositedAt = DateTime.UtcNow;
    }
}
