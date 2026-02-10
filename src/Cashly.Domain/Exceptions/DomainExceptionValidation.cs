namespace Cashly.Domain.Exceptions
{
    public class DomainExceptionValidation : Exception
    {
        public DomainExceptionValidation(string message) : base(message) { }

        public static void When(bool hasError, string message)
        {
            if (hasError)
                throw new DomainExceptionValidation(message);
        }
    }
}
