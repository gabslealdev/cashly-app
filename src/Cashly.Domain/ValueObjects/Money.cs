namespace Cashly.Domain.ValueObjects
{
    public sealed record Money
    {
        public decimal Value { get; }

        private Money(decimal value)
        {
            Value = decimal.Round(value, 2, MidpointRounding.AwayFromZero);
        }

        private Money() { }

        public static Money Create(decimal value) => new Money(value);

        public static Money operator +(Money a, Money b) => new(a.Value + b.Value);

        public static Money operator -(Money a, Money b) => new(a.Value - b.Value);

        public bool isZero() => Value == 0m;

        public override string ToString() => Value.ToString("N2");

    }
}
