using Cashly.Domain.Exceptions;

namespace Cashly.Domain.ValueObjects
{
    public sealed record class Email
    {
        public string Value { get; private set; }

        private Email(string value)
        {
            value = Normalize(value);
            Validate(value);
            Value = value;
        }
        private Email(){}

        public static Email Create(string value) 
            => new Email(value);

        private static string Normalize(string value) => value?.Trim().ToLower() ?? string.Empty;

        private static void Validate(string value)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(value), "Email is required");
        }

        public override string ToString() => Value;

    }
}
