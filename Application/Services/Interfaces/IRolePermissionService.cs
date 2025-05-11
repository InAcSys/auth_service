using AuthService.Domain.DTOs.RolePermission;
using AuthService.Domain.Entities.Concretes;

namespace AuthService.Application.Services.Interfaces
{
    public interface IRolePermissionService : IService<RolePermission, int>
    {
        Task<RolePermissionsDTO> GetPermissionsByRoleId(int roleId, Guid tenantId);
        Task<RolePermissionsDTO> Assign(int id, CreateRolePermissionDTO permissions, Guid tenantId);
        Task<bool> HasPermission(HasPermissionDTO hasPermission);
        Task<bool> RevokePermissions(int id, PermissionsDTO permissions, Guid tenantId);
    }
}
