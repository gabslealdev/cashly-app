using Cashly.Domain.Exceptions;

namespace Cashly.Domain.ValueObjects
{
    public sealed record Money
    {
        public decimal Value { get; private set; }

        public Money(decimal value)
        {
            Value = value;
        }

        private Money() { }

        public static Money Create(decimal value)
        {
            return new Money(value);
        }


        public static Money operator +(Money a, Money b) => new(a.Value + b.Value);

        public static Money operator -(Money a, Money b) => new(a.Value - b.Value);


        public override string ToString() => $"R$ {Value:N2}";

    }
}
