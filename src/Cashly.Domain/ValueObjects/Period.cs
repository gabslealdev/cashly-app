using Cashly.Domain.Exceptions;

namespace Cashly.Domain.ValueObjects
{
    public sealed record Period
    {
        public int Year { get; private set; }
        public int Month { get; private set; }

        private Period(int year, int month)
        {
            Year = year;
            Month = month;
        }

        private Period() { }

        public static void Validate(int year, int month)
        {
            DomainExceptionValidation.When(year < 1, "Year must be greater than 0");
            DomainExceptionValidation.When(month < 1 || month > 12, "Month must be between 1 and 12");
        }

        public static Period Create(int year, int month)
        {
            return new Period(year, month);
        }
         public override string ToString() => $"{Year:D4}-{Month:D2}";
    }
}
