using Cashly.Domain.Entities.Bases;
using Cashly.Domain.ValueObjects;

namespace Cashly.Domain.Entities
{
    public sealed class Category : Entity
    {
        public Key Key { get; private set; } = null!;
        public Title Title { get; private set; } = null!;

        public Category() { }  

    }
}
