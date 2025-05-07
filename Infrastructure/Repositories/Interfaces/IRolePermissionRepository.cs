using AuthService.Domain.DTOs.RolePermission;
using AuthService.Domain.Entities.Concretes;

namespace AuthService.Infrastructure.Repositories.Interfaces
{
    public interface IRolePermissionRepository : IRepository<RolePermission, int>
    {
        Task<RolePermissionsDTO> GetPermissionsByRoleId(int roleId);
        Task<RolePermissionsDTO> Assign(int id, CreateRolePermissionDTO permissions);
        Task<bool> HasPermission(HasPermissionDTO hasPermission);
        Task<bool> Exists(int roleId, int permissionId);
    }
}
