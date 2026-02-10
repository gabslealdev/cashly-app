using Cashly.Domain.Exceptions;

namespace Cashly.Domain.ValueObjects
{
    public sealed record Name
    {
        public string FirstName { get; private set; } 
        public string LastName { get; private set; }

        private Name(string firstName, string Lastname)
        {
            FirstName = firstName;
            LastName = Lastname;
        }

        private Name(){}

        public static Name Create(string firstName, string lastName)
        {
            firstName = Normalize(firstName);
            lastName = Normalize(lastName);
            Validate(firstName, lastName);
            return new Name(firstName, lastName);
        }

        private static string Normalize(string value) => value?.Trim() ?? string.Empty;

        private static void Validate(string firstName, string lastName)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(firstName), "First name is required");
            DomainExceptionValidation.When(firstName.Length < 3, "First name must be at least 3 characters long"); ;
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(lastName), "Last name is required");
            DomainExceptionValidation.When(lastName.Length < 3, "Last name must be at least 3 characters long");
        }
        
        public override string ToString() => $"{FirstName} {LastName}";

    }
}
