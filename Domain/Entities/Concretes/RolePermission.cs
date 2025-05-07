using AuthService.Domain.Entities.Abstracts;

namespace AuthService.Domain.Entities.Concretes
{
    public class RolePermission : Entity<int>
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
    }
}
