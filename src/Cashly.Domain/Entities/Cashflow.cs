using Cashly.Domain.Entities.Bases;
using Cashly.Domain.Enums;
using Cashly.Domain.Exceptions;
using Cashly.Domain.ValueObjects;

namespace Cashly.Domain.Entities
{
    public sealed class Cashflow : Entity
    {
        private readonly List<Transaction> _transactions = new();
        private readonly List<CashflowMember> _members = new();
        private readonly List<ClosedMonth> _closedMonths = new();

        public Title Title { get; private set; } = null!;
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }

        public IReadOnlyCollection<Transaction> Transactions => _transactions;
        public IReadOnlyCollection<CashflowMember> Members => _members;
        public IReadOnlyCollection<ClosedMonth> ClosedMonths => _closedMonths;

        private Cashflow(Guid id, string title, Guid userId) : base(id)
        {
            Title = Title.Create(title);
            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = DateTimeOffset.UtcNow;
            AssignOwner(userId);
        }

        private Cashflow() { }

        public static Cashflow Create(string title, Guid userId)
            => new Cashflow(Guid.NewGuid(), title, userId);

        public Guid AddTransaction(string title, decimal amount, TransactionType type, Guid categoryId, TransactionStatus status, DateTimeOffset occurredAt)
        { 
            var period = Period.Create(occurredAt.Year, occurredAt.Month);
            DomainExceptionValidation.When(IsMonthClosed(period), "This month is closed.");

            var transaction = Transaction.Create(this.Id, title, amount, type, categoryId, status, occurredAt);
            _transactions.Add(transaction);

            UpdatedAt = DateTimeOffset.UtcNow;

            return transaction.Id;

        }
         
        public void UpdateTransaction(Guid transactionId, string title, decimal amount, Guid categoryId, DateTimeOffset occurredAt)
        {
            DomainExceptionValidation.When(transactionId == Guid.Empty, "Transaction reference cannot be empty.");
            var transaction = GetTransaction(transactionId);

            var period = Period.Create(transaction.OccurredAt.Year, transaction.OccurredAt.Month);
            DomainExceptionValidation.When(IsMonthClosed(period), "This month is closed.");


            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(title), "Title cannot be empty.");
            transaction.Rename(title);

            DomainExceptionValidation.When(amount <= 0, "Amount must be greater than zero.");
            transaction.ChangeAmout(amount);

            DomainExceptionValidation.When(categoryId == Guid.Empty, "Category reference cannot be null.");
            transaction.ChangeCategory(categoryId);

            RescheduleTransaction(transactionId, occurredAt);
            UpdatedAt = DateTimeOffset.UtcNow;

        }

        public void DeleteTransaction(Guid transactionId)
        {
            DomainExceptionValidation.When(transactionId == Guid.Empty, "Transaction reference cannot be empty.");
            var transaction = GetTransaction(transactionId);

            var period = Period.Create(transaction.OccurredAt.Year, transaction.OccurredAt.Month);
            DomainExceptionValidation.When(IsMonthClosed(period), "This month is closed.");

            _transactions.Remove(transaction);
            UpdatedAt = DateTimeOffset.UtcNow;

        }

        public void RescheduleTransaction(Guid transactionId, DateTimeOffset OcurredAt)
        {
            DomainExceptionValidation.When(transactionId == Guid.Empty, "Transaction reference cannot be empty.");
            var transaction = GetTransaction(transactionId);

            var currentPeriod = Period.Create(transaction.OccurredAt.Year, transaction.OccurredAt.Month);
            DomainExceptionValidation.When(IsMonthClosed(currentPeriod), "Cannot modify a transaction from a closed month.");

            var targetPeriod = Period.Create(OcurredAt.Year, OcurredAt.Month);
            DomainExceptionValidation.When(IsMonthClosed(targetPeriod), "Cannot move transaction to a closed month.");

            transaction.Reschedule(OcurredAt);
        }

        public void MarkTransactionAsCompleted(Guid transactionId)
        {
            DomainExceptionValidation.When(transactionId == Guid.Empty, "Transaction reference cannot be empty.");
            var transaction = GetTransaction(transactionId);

            if (transaction.Status == TransactionStatus.Scheduled)
            {
                transaction.MarkAsCompleted();
                UpdatedAt = DateTimeOffset.UtcNow;
            }
        }

        public void MarkTransactionAsScheduled(Guid transactionId)
        {
            DomainExceptionValidation.When(transactionId == Guid.Empty, "Transaction reference cannot be empty.");
            var transaction = GetTransaction(transactionId);

            var period = Period.Create(transaction.OccurredAt.Year, transaction.OccurredAt.Month);
            DomainExceptionValidation.When(IsMonthClosed(period), "Cannot modify a transaction from a closed month.");

            if (transaction.Status == TransactionStatus.Completed)
            {
                transaction.MarkAsScheduled();
                UpdatedAt = DateTimeOffset.UtcNow;
            }
        }

        public void MarkTransactionAsCanceled(Guid transactionId) 
        {
            DomainExceptionValidation.When(transactionId == Guid.Empty, "Transaction reference cannot be empty.");
            var transaction = GetTransaction(transactionId);

            transaction.Cancel();
        }

        public void CloseMonth(int year, int month)
        {
            var period = Period.Create(year, month);

            DomainExceptionValidation.When(period.isFuture(), "Future months cannot be closed."); 
            
            DomainExceptionValidation.When(_closedMonths.Any(c => c.Period == period), "This month is already closed.");

            var transactions = GetTransactionsByPeriod(period);
            DomainExceptionValidation.When(transactions.Any(t => t.Status == TransactionStatus.Scheduled), "The month cannot be closed because there are scheduled transactions.");

            var completed = transactions.Where(t => t.Status == TransactionStatus.Completed);

            var totalExpense = completed.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount.Value);
            var totalIncome = completed.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount.Value);
            
            var closedMonth = ClosedMonth.Create(this.Id, year, month, totalExpense, totalIncome);
            _closedMonths.Add(closedMonth);
        }

        private void AssignOwner(Guid userId)
        {
            DomainExceptionValidation.When(_members.Any(m => m.Role == UserRole.Owner), "Cashflow already has an owner.");
            var owner = CashflowMember.CreateOwner(this.Id, userId);
            _members.Add(owner);
            UpdatedAt = DateTime.UtcNow;
        }

        private Transaction GetTransaction(Guid transactionId)
        {
            var transaction = _transactions.FirstOrDefault(t => t.Id == transactionId);
            DomainExceptionValidation.When(transaction is null, "Transction cannot be null.");

            return transaction!;
        }

        private IEnumerable<Transaction> GetTransactionsByPeriod(Period period)
            => _transactions.Where(t => Period.FromDate(t.OccurredAt) == period);
    
        private bool IsMonthClosed(Period period)
            => _closedMonths.Any(m => m.Period == period);




    }
}
