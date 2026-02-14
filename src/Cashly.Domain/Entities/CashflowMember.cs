using Cashly.Domain.Enums;

namespace Cashly.Domain.Entities
{
    public sealed class CashflowMember
    {
        public Guid CashflowId { get; private set; }
        public Guid UserId { get; private set; }
        public UserRole Role { get; private set; }
        public DateTimeOffset JoinedAt { get; private set; }

        private CashflowMember(Guid cashflowId, Guid userId, UserRole role)
        {
            CashflowId = cashflowId;
            UserId = userId;
            Role = role;
            JoinedAt = DateTimeOffset.UtcNow;
        }

        private CashflowMember() { }

        internal static CashflowMember CreateMember(Guid cashflowId, Guid userId, UserRole role)
            => new CashflowMember(cashflowId, userId, role);
    }
}
