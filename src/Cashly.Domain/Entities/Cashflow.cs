using Cashly.Domain.Entities.Bases;
using Cashly.Domain.ValueObjects;

namespace Cashly.Domain.Entities
{
    public sealed class Cashflow : Entity
    {

        public Title Title { get; private set; } = null!;
        public DateTimeOffset CreateadAt { get; private set; }
        public DateTimeOffset UpdateadAt { get; private set; }

        private Cashflow(Guid id, Title title) : base(id)
        {
            Title = title;
            CreateadAt = DateTimeOffset.UtcNow;
            UpdateadAt = DateTimeOffset.UtcNow;
        }

        private Cashflow() { }

        public static Cashflow Create(Title title)
            => new Cashflow(Guid.NewGuid(), title);

    }
}
