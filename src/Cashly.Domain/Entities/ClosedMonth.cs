using Cashly.Domain.Entities.Bases;
using Cashly.Domain.Enums;
using Cashly.Domain.ValueObjects;

namespace Cashly.Domain.Entities
{
    public sealed class ClosedMonth : Entity
    {
        public Guid CashflowId { get; private set; }
        public Period Period { get; private set; }
        public Money TotalIncome { get; private set; }
        public Money TotalExpense { get; private set; }
        public Money Balance { get; private set; }
        public HealthStatus HealthStatus { get; private set; }
        public DateTimeOffset ClosedAt { get; private set; }

        private ClosedMonth(Guid id, Guid clashflowId, int year, int month, decimal totalIncome, decimal totalExpense ) : base( id )
        {
            Id = id;
            CashflowId = clashflowId;
            Period = Period.Create(year, month);
            TotalIncome = Money.Create(totalIncome);
            TotalExpense = Money.Create(totalExpense);
            Balance = TotalIncome - TotalExpense;
            HealthStatus = CalculateHealthStatus();

        }

        private ClosedMonth() { }

        private HealthStatus CalculateHealthStatus()
        {
            if (TotalIncome.isZero() && TotalExpense.isZero())
                return HealthStatus.Gray;

            if (TotalIncome.isZero() && TotalExpense.Value > 0)
                return HealthStatus.Red;

            var percentage = ((TotalIncome.Value - TotalExpense.Value) / TotalIncome.Value) * 100;

            if (percentage <= 0.0m)
                return HealthStatus.Red;

            if (percentage <= 5.0m)
                return HealthStatus.Orange;

            if (percentage <= 10.0m )
                return HealthStatus.Yellow;

            if (percentage <= 20.0m) 
                return HealthStatus.Green;

            return HealthStatus.Blue;
        }
        internal static ClosedMonth Create(Guid clashflowId, int year, int month, decimal totalIncome, decimal totalExpense)
            => new ClosedMonth(Guid.NewGuid(), clashflowId, year, month, totalIncome, totalExpense);


    }
}
