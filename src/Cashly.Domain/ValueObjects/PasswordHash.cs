using Cashly.Domain.Exceptions;

namespace Cashly.Domain.ValueObjects
{
    public sealed record PasswordHash
    {
        public string Value { get; private set; }

        private PasswordHash(string value)
        {
            Value = value;
        }

        public static PasswordHash Create(string passwordHash)
        {
            Validate(passwordHash);
            return new PasswordHash(passwordHash);
        }

        private PasswordHash() { }

        private static void Validate(string passwordHash)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(passwordHash), "Password hash is required");
        }


    }
}
