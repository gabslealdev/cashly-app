using Cashly.Domain.Entities.Bases;
using Cashly.Domain.Enums;
using Cashly.Domain.ValueObjects;

namespace Cashly.Domain.Entities
{
    public sealed class Transaction : Entity
    {
        public Guid CashflowId { get; private set; }
        public Title Title { get; private set; }
        public Money Amount { get; private set; }
        public TransactionType Type { get; private set; }
        public Guid CategoryId { get; private set; }
        public TransactionStatus Status { get; private set; }
        public DateTimeOffset OcurredAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }
        

        private Transaction(Guid id, Guid cashflowId, string title, decimal amount,TransactionType type, Guid categoryId, TransactionStatus status, DateTimeOffset ocurredAt) : base(id)
        {
            Id = id;
            CashflowId = cashflowId;
            Title = Title.Create(title);
            Amount = Money.Create(amount);
            Type = type;
            CategoryId = categoryId;
            OcurredAt = ocurredAt;
            Status = status;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        private Transaction(){}

        public static Transaction Create(Guid id,Guid cashflowId, string title, decimal amount, TransactionType type, Guid categoryId, TransactionStatus status, DateTimeOffset ocurredAt)
            => new Transaction(Guid.NewGuid(), cashflowId, title, amount, type, categoryId, status, ocurredAt);

        public void Rename(string title)
        {
            Title = Title.Create(title);
            UpdatedAt = DateTimeOffset.UtcNow;
        }
            
        public void ChangeAmout(decimal value)
        {
            Amount = Money.Create(value);
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void ChangeCategory(Guid categoryId)
        {
            CategoryId = categoryId;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void Rescheduled(DateTimeOffset ocurredAt)
        {
            OcurredAt = ocurredAt;
            Status = TransactionStatus.Scheduled;
            UpdatedAt = DateTimeOffset.UtcNow;

        }

        public void MarkAsCompleted()
        {
            Status = TransactionStatus.Completed;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void MarkAsScheduled()
        {
            Status = TransactionStatus.Scheduled;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void Cancel()
        {
            Status = TransactionStatus.Canceled;
            UpdatedAt = DateTimeOffset.UtcNow;
        }
    
    }
}
