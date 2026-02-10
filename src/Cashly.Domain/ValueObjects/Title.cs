using Cashly.Domain.Exceptions;

namespace Cashly.Domain.ValueObjects
{
    public sealed record Title
    {
        public string Value { get; private set; }

        public Title(string value)
        {
            Value = value;
        }
        private Title() { }

        public static Title Create(string value)
        {
            value = Normalize(value);
            Validate(value);
            return new Title(value);
        }

        private static string Normalize(string value) => value?.Trim() ?? string.Empty;

        private static void Validate(string value)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(value), "Title is required");
            DomainExceptionValidation.When(value.Length < 3, "Title must be at least 3 characters long");
        }
    }
}
