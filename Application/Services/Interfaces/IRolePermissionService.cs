using AuthService.Domain.DTOs.RolePermission;
using AuthService.Domain.Entities.Concretes;

namespace AuthService.Application.Services.Interfaces
{
    public interface IRolePermissionService : IService<RolePermission, int>
    {
        Task<RolePermissionsDTO> GetPermissionsByRoleId(int roleId);
        Task<RolePermissionsDTO> Assign(int id, CreateRolePermissionDTO permissions);
        Task<bool> HasPermission(HasPermissionDTO hasPermission);
    }
}
