using AuthService.Domain.DTOs.RolePermission;
using AuthService.Domain.Entities.Concretes;

namespace AuthService.Infrastructure.Repositories.Interfaces
{
    public interface IRolePermissionRepository : IRepository<RolePermission, int>
    {
        Task<RolePermissionsDTO> GetPermissionsByRoleId(int roleId, Guid tenantId);
        Task<RolePermissionsDTO> Assign(int id, CreateRolePermissionDTO permissions, Guid tenantId);
        Task<bool> HasPermission(VerifyPermissionDTO hasPermission);
        Task<int> Exists(int roleId, int permissionId, Guid tenantId);
        Task<bool> RevokePermissions(int id, PermissionsDTO permissions, Guid tenantId);
    }
}
