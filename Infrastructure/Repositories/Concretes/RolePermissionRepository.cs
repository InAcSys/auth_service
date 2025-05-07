using AuthService.Domain.Entities.Concretes;
using AuthService.Infrastructure.Repositories.Abstracts;
using AuthService.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Repositories.Concretes
{
    public class RolePermissionRepository(
        DbContext context,
        IRepository<Role, int> roleRepository,
        IRepository<Permission, int> permissionRepository
    ) : AbstractRolePermissionRepository(context, roleRepository, permissionRepository)
    {
    }
}
