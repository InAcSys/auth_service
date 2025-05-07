using System.ComponentModel.DataAnnotations;
using AuthService.Domain.Entities.Interfaces;

namespace AuthService.Domain.Entities.Abstracts
{
    public abstract class Entity<TKey> : IEntity<TKey>
    {
        [Key]
        public TKey? Id { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }
        public DateTime? Deleted { get; set; }
        public Guid TenantId { get; set; }
    }
}
