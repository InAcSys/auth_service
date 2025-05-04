using AuthService.Domain.Entities.Interfaces;

namespace AuthService.Domain.Entities.Abstracts
{
    public abstract class Entity<TKey> : IEntity<TKey>
    {
        public TKey? Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Deleted { get; set; }
        public Guid TenantId { get; set; }
    }
}
