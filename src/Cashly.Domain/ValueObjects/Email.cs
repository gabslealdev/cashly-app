using Cashly.Domain.Exceptions;

namespace Cashly.Domain.ValueObjects
{
    public sealed record class Email
    {
        public string Value { get; private set; }

        private Email(string value)
        {
            Value = value;
        }
        private Email() { }

        public static Email Create(string email)
        {
            Normalize(email);
            Validate(email);
            return new Email(email);
        }
        private static string Normalize(string value) => value?.Trim().ToLower() ?? string.Empty;

        private static void Validate(string email)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(email), "Email is required");
            DomainExceptionValidation.When(!email.Contains("@"), "Email is invalid");
        }

        public override string ToString() => Value;

    }
}
