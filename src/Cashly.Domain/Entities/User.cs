using Cashly.Domain.Entities.Bases;
using Cashly.Domain.ValueObjects;

namespace Cashly.Domain.Entities
{
    public sealed class User : Entity
    {
        public Name Name { get;  private set; } = null!;
        public Email Email { get; private set; } = null!;
        public PasswordHash PasswordHash { get; private set; } = null!;
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }

        private User(Guid id, string firstName, string lastName, string email, string passwordHash) : base(id)
        {
            Name = Name.Create(firstName, lastName);  
            Email = Email.Create(email);
            PasswordHash = PasswordHash.Create(passwordHash);
            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        private User() { }

        public static User Create(string firstName, string lastName, string email, string passwordHash)
            => new User(Guid.NewGuid(), firstName, lastName, email, passwordHash);

        public void ChangeName(string firstName, string lastName)
        {
            Name = Name.Create(firstName, lastName);
            UpdatedAt = DateTimeOffset.UtcNow;           
        }

        public void ChangePasswordHash(string passwordHash)
        {
            PasswordHash = PasswordHash.Create(passwordHash);
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void ChangeEmail(string email)
        {
            Email = Email.Create(email);
            UpdatedAt = DateTimeOffset.UtcNow;
        }
        
    }
}
