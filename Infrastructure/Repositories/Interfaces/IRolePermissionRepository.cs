using AuthService.Domain.DTOs.RolePermission;
using AuthService.Domain.Entities.Concretes;

namespace AuthService.Infrastructure.Repositories.Interfaces
{
    public interface IRolePermissionRepository : IRepository<RolePermission, int>
    {
        Task<RolePermissionsDTO> GetPermissionsByRoleId(int roleId);
        Task<RolePermissionsDTO> Assign(int id, CreateRolePermissionDTO permissions);
        Task<bool> HasPermission(VerifyPermissionDTO hasPermission);
        Task<int> Exists(int roleId, int permissionId);
        Task<bool> RevokePermissions(int id, PermissionsDTO permissions);
    }
}
