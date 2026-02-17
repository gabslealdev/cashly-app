using Cashly.Domain.Enums;
using Cashly.Domain.Exceptions;

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
            Validate(cashflowId, userId, role);
            CashflowId = cashflowId;
            UserId = userId;
            Role = role;
            JoinedAt = DateTimeOffset.UtcNow;
        }

        private CashflowMember() { }

        private static CashflowMember Create(Guid cashflowId, Guid userId, UserRole role)
        {
            return new CashflowMember(cashflowId, userId, role);
        }

        private static void Validate(Guid cashflowId, Guid userId, UserRole role)
        {
            DomainExceptionValidation.When(cashflowId == Guid.Empty, "Cashflow reference is required");
            DomainExceptionValidation.When(userId == Guid.Empty, "User reference is required");
            DomainExceptionValidation.When(!Enum.IsDefined(typeof(UserRole), role), "Invalid role");

        }
            
        internal static CashflowMember CreateOwner(Guid cashflowId, Guid userId)
            => Create(cashflowId, userId, UserRole.Owner);

        internal static CashflowMember CreateContributor(Guid cashflowId, Guid userId)
            => Create(cashflowId, userId, UserRole.Contributor);

        internal static CashflowMember CreateViewer(Guid cashflowId, Guid userId)
            => Create(cashflowId, userId, UserRole.Viewer);

    }
}
