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

        private User(Guid id, Name name, Email email, PasswordHash passwordHash) : base(id)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        private User() { }

        public static User Create(Name name, Email email, PasswordHash passwordHash)
            => new User(Guid.NewGuid(), name, email, passwordHash);

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
