using Cashly.Domain.Exceptions;

namespace Cashly.Domain.ValueObjects
{
    public sealed record Period : IComparable<Period>
    {
        public int Year { get; private set; }
        public int Month { get; private set; }

        private Period(int year, int month)
        {
            Validate(year, month);
            Year = year;
            Month = month;
        }

        private Period() { }

        public static void Validate(int year, int month)
        {
            DomainExceptionValidation.When(year < 1, "Year must be greater than 0");
            DomainExceptionValidation.When(month < 1 || month > 12, "Month must be between 1 and 12");
        }

        public static Period FromDate(DateTimeOffset date) 
            => Create(date.Year, date.Month);  

        public static Period Create(int year, int month) 
            => new Period(year, month);

        public bool isFuture()
            => this > Current();

        public static Period Current()
            => Create(DateTimeOffset.UtcNow.Year, DateTimeOffset.UtcNow.Month);

         public override string ToString() 
            => $"{Year:D4}-{Month:D2}";

        public int CompareTo(Period? other)
        {
            if (other is null)
                return 1;

            var yearComparsion = Year.CompareTo(other.Year);
            if( yearComparsion != 0 )
                return yearComparsion;

            return Month.CompareTo(other.Month);
        }

        public static bool operator >(Period left, Period right)
            => left.CompareTo(right) > 0;

        public static bool operator <(Period left, Period right)
            => left.CompareTo(right) < 0;

        public static bool operator >=(Period left, Period right)
            => left.CompareTo(right) >= 0;

        public static bool operator <=(Period left, Period right)
            => left.CompareTo(right) <= 0;

    }
}
