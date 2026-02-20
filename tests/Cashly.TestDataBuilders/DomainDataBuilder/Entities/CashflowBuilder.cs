using Bogus;
using Cashly.Domain.Entities;
using Cashly.Domain.Enums;


namespace Cashly.TestDataBuilders.DomainDataBuilder.Entities
{
    public static class CashflowBuilder
    {
        public static Cashflow Build()
        {
            var faker = new Faker("pt_BR");
            var user = UserBuilder.Build();

            return Cashflow.Create(
                title: faker.Name.LastName(),
                userId: user.Id
                );
            
        }

        public static Guid WithTransaction(Cashflow cashflow)
        {
           var faker = new Faker();

            return cashflow.AddTransaction(
                        title: faker.Commerce.ProductName(),
                        amount: faker.Random.Decimal(100.00m, 600.00m),
                        type: TransactionType.Expense,
                        categoryId: Guid.NewGuid(),
                        status: TransactionStatus.Completed,
                        occurredAt: DateTimeOffset.UtcNow
                );
        }

        public static Guid WithScheduledTransaction(Cashflow cashflow)
        {
            var faker = new Faker();

            return cashflow.AddTransaction(
                        title: faker.Commerce.ProductName(),
                        amount: faker.Random.Decimal(100.00m, 600.00m),
                        type: TransactionType.Expense,
                        categoryId: Guid.NewGuid(),
                        status: TransactionStatus.Scheduled,
                        occurredAt: DateTimeOffset.UtcNow
                );

        }

    }
}
