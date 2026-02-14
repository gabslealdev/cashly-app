using Cashly.Domain.Entities.Bases;

namespace Cashly.Domain.Entities
{
    public sealed class ClosedMonth : Entity
    {
        public Guid CashflowId { get; set; }
    }
}
