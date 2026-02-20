using Cashly.Domain.Enums;
using Cashly.Domain.Exceptions;
using Cashly.TestDataBuilders.DomainDataBuilder.Entities;
using Shouldly;

namespace Cashflow.Domain.Tests.Entities
{
    public class CashflowUnitTest
    {
        [Fact]
        public void AddTransaction_ShouldAdd_WhenValid()
        {
            // arrange
            var cashflow = CashflowBuilder.Build();
            var title = "Mensalidade da Academia";
            var amount = 140.00m;
            var type = TransactionType.Expense;
            var categoryId = Guid.NewGuid();
            var status = TransactionStatus.Completed;
            var occurredAt = DateTimeOffset.UtcNow;
            
            // act
            Action action = () => cashflow.AddTransaction(title, amount, type, categoryId, status, occurredAt);

            // assert
            action.ShouldNotThrow();
        }

        [Fact]
        public void AddTransaction_ShouldThrow_WhenMonthIsClosed()
        {
            // arrange
            var cashflow = CashflowBuilder.Build();
            var now = DateTimeOffset.UtcNow;
            cashflow.CloseMonth(now.Year, now.Month);
            var title = "Mensalidade da Academia";
            var amount = 140.00m;
            var type = TransactionType.Expense;
            var categoryId = Guid.NewGuid();
            var status = TransactionStatus.Completed;
            var occurredAt = now;

            // act 
            Action action = () => cashflow.AddTransaction(title, amount, type, categoryId, status, occurredAt);

            // assert
            action.ShouldThrow<DomainExceptionValidation>().Message.ShouldBe("This month is closed.");

        }

        [Fact]
        public void UpdateTransaction_ShouldUpdate_WhenValid()
        {
            // arrange
            var cashflow = CashflowBuilder.Build();
            var transactionId = CashflowBuilder.WithTransaction(cashflow);
            var title = "Dentista";
            var amount = 350.00m;
            var categoryId = Guid.NewGuid();
            var occurredAt = DateTimeOffset.UtcNow; 

            // act
            Action action = () => cashflow.UpdateTransaction(transactionId, title, amount, categoryId, occurredAt);

            // assert
            action.ShouldNotThrow();
        }

        [Fact]
        public void UpdateTransaction_ShouldThrow_WhenMonthIsClosed()
        {
            // arrange
            var now = DateTimeOffset.UtcNow;
            var cashflow = CashflowBuilder.Build();
            var transactionId = CashflowBuilder.WithTransaction(cashflow);

            cashflow.CloseMonth(now.Year, now.Month);

            var title = "Jiu-Jitsu Academy";
            var amount = 180.00m;
            var categoryId = Guid.NewGuid();
            var occurredAt = now;

            // act
            Action action = () => cashflow.UpdateTransaction(transactionId, title, amount, categoryId, occurredAt);

            // assert
            action.ShouldThrow<DomainExceptionValidation>().Message.ShouldBe("This month is closed.");

        }

        [Fact]
        public void UpdateTransaction_ShouldThrow_WhenTransactionIsNotValid()
        {
            // arrange
            var cashflow = CashflowBuilder.Build();
            var transactionId = Guid.Empty;
            var title = "Football tickets";
            var amount = 100.00m;
            var categoryId = Guid.NewGuid();
            var occurredAt = DateTimeOffset.UtcNow;

            // act
            Action action = () => cashflow.UpdateTransaction(transactionId, title, amount, categoryId, occurredAt);

            // assert
            action.ShouldThrow<DomainExceptionValidation>().Message.ShouldBe("Transaction reference cannot be empty.");
        }

        [Fact]
        public void UpdateTransaction_ShouldThrow_WhenTitleIsNotValid()
        {
            // arrange
            var cashflow = CashflowBuilder.Build();
            var transactionId = CashflowBuilder.WithTransaction(cashflow);
            var title = "  ";
            var amount = 400.00m;
            var categoryId = Guid.NewGuid();
            var occurredAt = DateTimeOffset.UtcNow;

            // act
            Action action = () => cashflow.UpdateTransaction(transactionId, title, amount, categoryId, occurredAt);

            // assert
            action.ShouldThrow<DomainExceptionValidation>().Message.ShouldBe("Title cannot be empty.");
        }

        [Fact]
        public void UpdateTransaction_ShouldThrow_WhenAmountIsNotValid()
        {
            // arrange
            var cashflow = CashflowBuilder.Build();
            var transactionId = CashflowBuilder.WithTransaction(cashflow);
            var title = "Hotel";
            var amount = -500.00m;
            var categoryId = Guid.NewGuid();
            var occurredAt = DateTimeOffset.UtcNow;

            // act
            Action action = () => cashflow.UpdateTransaction(transactionId, title, amount, categoryId, occurredAt);

            // assert
            action.ShouldThrow<DomainExceptionValidation>().Message.ShouldBe("Amount must be greater than zero.");
        }

        [Fact]
        public void UpdateTransaction_ShouldThrow_WhenCategoryIsNotValid()
        {
            // arrange
            var cashflow = CashflowBuilder.Build();
            var transactionId = CashflowBuilder.WithTransaction(cashflow);
            var title = "Dinner with friends";
            var amount = 70.00m;
            var categoryId = Guid.Empty;
            var occurredAt = DateTimeOffset.UtcNow;

            // act
            Action action = () => cashflow.UpdateTransaction(transactionId, title, amount, categoryId, occurredAt);

            // assert
            action.ShouldThrow<DomainExceptionValidation>().Message.ShouldBe("Category reference cannot be null.");
        }

        [Fact]
        public void DeleteTransaction_ShouldDelete_WhenIsValid()
        {
            // arrange
            var cashflow = CashflowBuilder.Build();
            var transactionId = CashflowBuilder.WithTransaction(cashflow);

            // act
            Action action = () => cashflow.DeleteTransaction(transactionId);

            // assert
            action.ShouldNotThrow();
        }

        [Fact]
        public void DeleteTransaction_ShouldThrow_WhenTransctionIsNotValid()
        {
            // arrange 
            var cashflow = CashflowBuilder.Build();
            var transactionId = Guid.Empty;

            // act 
            Action action = () => cashflow.DeleteTransaction(transactionId);

            // assert
            action.ShouldThrow<DomainExceptionValidation>().Message.ShouldBe("Transaction reference cannot be empty.");
        }

        [Fact]
        public void DeleteTransaction_ShouldThrow_WhenMonthIsClosed()
        {
            // arrange
            var now = DateTimeOffset.UtcNow;
            var cashflow = CashflowBuilder.Build();
            var transactionId = CashflowBuilder.WithTransaction(cashflow);
            cashflow.CloseMonth(now.Year, now.Month);

            // act
            Action action = () => cashflow.DeleteTransaction(transactionId);

            // assert
            action.ShouldThrow<DomainExceptionValidation>().Message.ShouldBe("This month is closed.");

        }

        [Fact]
        public void RescheduleTransaction_ShouldReschedule_WhenIsValid()
        {
            // arrange
            var date = DateTimeOffset.UtcNow;
            var cashflow = CashflowBuilder.Build();
            var transactionId = CashflowBuilder.WithTransaction(cashflow);

            // act
            Action action = () => cashflow.RescheduleTransaction(transactionId, date.AddDays(5));

            // assert
            action.ShouldNotThrow();


        }

        [Fact]
        public void RescheduleTransaction_ShouldThrow_WhenTransactionIsNotValid()
        {
            // arrange
            var date = DateTimeOffset.UtcNow;
            var cashflow = CashflowBuilder.Build();
            var transactionId = Guid.Empty;

            // act
            Action action = () => cashflow.RescheduleTransaction(transactionId, date.AddDays(5));

            // assert
            action.ShouldThrow<DomainExceptionValidation>().Message.ShouldBe("Transaction reference cannot be empty.");

        }

        [Fact]
        public void RescheduleTransaction_ShouldThrow_WhenCurrentPeriodIsClosed()
        {
            // arrange
            var date = DateTimeOffset.UtcNow;
            var cashflow = CashflowBuilder.Build();
            var transactionId = CashflowBuilder.WithTransaction(cashflow);
            cashflow.CloseMonth(date.Year, date.Month);

            // act 
            Action action = () => cashflow.RescheduleTransaction(transactionId, date.AddDays(5));

            // assert
            action.ShouldThrow<DomainExceptionValidation>().Message.ShouldBe("Cannot modify a transaction from a closed month.");

        }

        [Fact]
        public void RescheduleTransaction_ShouldThrow_WhenTargetPeriodIsClosed()
        {
            // arrange 
            var date = DateTimeOffset.UtcNow;
            var cashflow = CashflowBuilder.Build();
            var transactionId = CashflowBuilder.WithTransaction(cashflow);
            cashflow.CloseMonth(date.Year, date.AddMonths(-1).Month);

            // act
            Action action = () => cashflow.RescheduleTransaction(transactionId, date.AddMonths(-1));

            // assert
            action.ShouldThrow<DomainExceptionValidation>().Message.ShouldBe("Cannot move transaction to a closed month.");

        }

        [Fact]
        public void CompleteTransaction_ShouldComplete_WhenIsValid()
        {
            // arrange
            var cashflow = CashflowBuilder.Build();
            var transactionId = CashflowBuilder.WithScheduledTransaction(cashflow);

            // act
            Action action = () => cashflow.MarkTransactionAsCompleted(transactionId);

            // assert
            action.ShouldNotThrow();
            
        }

        [Fact]
        public void CompleteTransaction_ShouldThrow_WhenTransactionIsNotValid()
        {
            // arrange 
            var cashflow = CashflowBuilder.Build();
            var transactionId = Guid.Empty;

            // act
            Action action = () => cashflow.MarkTransactionAsCompleted(transactionId);

            // assert
            action.ShouldThrow<DomainExceptionValidation>().Message.ShouldBe("Transaction reference cannot be empty.");
        }

        [Fact]
        public void ScheduleTransaction_ShouldSchedule_WhenIsValid()
        {
            // arrange 
            var cashflow = CashflowBuilder.Build();
            var transactionId = CashflowBuilder.WithTransaction(cashflow);

            // act
            Action action = () => cashflow.MarkTransactionAsScheduled(transactionId);

            // assert
            action.ShouldNotThrow();
        }

        [Fact]
        public void ScheduleTransaction_ShouldThrow_WhenTransactionIsNotValid()
        {
            // arrange 
            var cashflow = CashflowBuilder.Build();
            var transactionId = Guid.Empty;

            // act
            Action action = () => cashflow.MarkTransactionAsScheduled(transactionId);

            // assert
            action.ShouldThrow<DomainExceptionValidation>().Message.ShouldBe("Transaction reference cannot be empty.");
        }

        [Fact]
        public void ScheduleTransaction_ShouldThrow_WhenPeriodIsClosed()
        {
            // arrange 
            var date = DateTimeOffset.UtcNow;
            var cashflow = CashflowBuilder.Build();
            var transactionId = CashflowBuilder.WithTransaction(cashflow);
            cashflow.CloseMonth(date.Year, date.Month);

            // act
            Action action = () => cashflow.MarkTransactionAsScheduled(transactionId);

            // assert
            action.ShouldThrow<DomainExceptionValidation>().Message.ShouldBe("Cannot modify a transaction from a closed month.");
        }

        [Fact]
        public void CancelTransaction_ShouldCancel_WhenIsValid()
        {
            // arrange
            var cashflow = CashflowBuilder.Build();
            var transactionId = CashflowBuilder.WithTransaction(cashflow);


            // act
            Action action = () => cashflow.MarkTransactionAsCanceled(transactionId);

            // assert
            action.ShouldNotThrow();
        }

        [Fact]
        public void CancelTransaction_ShouldThrow_WhenTransactionIsNotValid()
        {
            // arrange
            var cashflow = CashflowBuilder.Build();
            var transactionId = Guid.Empty;

            // act
            Action action = () => cashflow.MarkTransactionAsCanceled(transactionId);

            // assert
            action.ShouldThrow<DomainExceptionValidation>().Message.ShouldBe("Transaction reference cannot be empty.");

        }

    }
}
