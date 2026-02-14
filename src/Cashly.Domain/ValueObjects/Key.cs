using Cashly.Domain.Exceptions;

namespace Cashly.Domain.ValueObjects
{
    public sealed record Key
    {
        public string Value { get; private set; } = string.Empty;

        private Key(string value)
        {
            value = Normalize(value);
            Validate(value);
            Value = value;
        }
        public Key(){}

        private static void Validate(string value)
        {
            DomainExceptionValidation.When(value == string.Empty, "Invalid Key.");
            DomainExceptionValidation.When(value.Length < 3, "Invalid Key.");           
        }

        private static string Normalize(string value) => value?.ToLower().Trim() ?? string.Empty;

        public static Key Create(string value)
            => new Key(value);

        public override string ToString() => Value;
    }
}
