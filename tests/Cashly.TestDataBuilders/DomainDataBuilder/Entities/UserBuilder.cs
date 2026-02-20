using Bogus;
using Cashly.Domain.Entities;

namespace Cashly.TestDataBuilders.DomainDataBuilder.Entities
{
    public static class UserBuilder
    {
        public static User Build()
        {
            var faker = new Faker("pt_BR");

            return User.Create(
                firstName: faker.Name.FirstName(), 
                lastName: faker.Name.LastName(), 
                email: faker.Internet.Email(), 
                passwordHash: faker.Random.Hash()
              );
        }
    }
}
