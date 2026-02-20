using Cashly.Domain.Entities.Bases;
using Cashly.Domain.Enums;
using Cashly.Domain.Exceptions;
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
        public DateTimeOffset OccurredAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }
        

        private Transaction(Guid id, Guid cashflowId, string title, decimal amount,TransactionType type, Guid categoryId, TransactionStatus status, DateTimeOffset occurredAt) : base(id)
        {
            Validate(cashflowId, amount, type, categoryId, status);
            CashflowId = cashflowId;
            Title = Title.Create(title);
            Amount = Money.Create(amount);
            Type = type;
            CategoryId = categoryId;
            OccurredAt = occurredAt;
            Status = status;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        private Transaction(){}

        private static void Validate(Guid cashflowId, decimal amount,  TransactionType type, Guid categoryId, TransactionStatus status)
        {
            DomainExceptionValidation.When(cashflowId == Guid.Empty, "Cashflow reference is required.");
            DomainExceptionValidation.When(amount <= 0, "Transaction value must be greater than 0.");
            DomainExceptionValidation.When(!Enum.IsDefined(typeof(TransactionType), type), "Invalid type.");
            DomainExceptionValidation.When(categoryId == Guid.Empty, "Category reference is required.");
            DomainExceptionValidation.When(!Enum.IsDefined(typeof(TransactionStatus), status), "Invalid status.");
        }

        internal static Transaction Create(Guid cashflowId, string title, decimal amount, TransactionType type, Guid categoryId, TransactionStatus status, DateTimeOffset occurredAt)
            => new Transaction(Guid.NewGuid(), cashflowId, title, amount, type, categoryId, status, occurredAt);

        internal void Rename(string title)
        {
            Title = Title.Create(title);
            UpdatedAt = DateTimeOffset.UtcNow;
        }
            
        internal void ChangeAmout(decimal value)
        {

            Amount = Money.Create(value);
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        internal void ChangeCategory(Guid categoryId)
        {
            CategoryId = categoryId;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        internal void Reschedule(DateTimeOffset occurredAt)
        {
            OccurredAt = occurredAt;
            Status = TransactionStatus.Scheduled;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        internal void MarkAsCompleted()
        {
            Status = TransactionStatus.Completed;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        internal void MarkAsScheduled()
        {
            Status = TransactionStatus.Scheduled;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        internal void Cancel()
        {
            Status = TransactionStatus.Canceled;
            UpdatedAt = DateTimeOffset.UtcNow;
        }
    
    }
}
