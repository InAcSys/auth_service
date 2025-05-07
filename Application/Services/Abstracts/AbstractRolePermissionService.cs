using AuthService.Application.Services.Interfaces;
using AuthService.Domain.DTOs.RolePermission;
using AuthService.Domain.Entities.Concretes;
using AuthService.Infrastructure.Repositories.Interfaces;
using FluentValidation;

namespace AuthService.Application.Services.Abstracts
{
    public abstract class AbstractRolePermissionService(
        IValidator<RolePermission> validator,
        IRepository<RolePermission, int> repository,
        IRolePermissionRepository rolePermissionRepository
    ) : Service<RolePermission, int>(validator, repository), IRolePermissionService
    {
        private readonly IRolePermissionRepository _rolePermissionRepository = rolePermissionRepository;
        public async Task<RolePermissionsDTO> GetPermissionsByRoleId(int roleId)
        {
            var result = await _rolePermissionRepository.GetPermissionsByRoleId(roleId);
            return result;
        }

        public async Task<RolePermissionsDTO> Assign(int id, CreateRolePermissionDTO permissions)
        {
            var result = await _rolePermissionRepository.Assign(id, permissions);
            return result;
        }

        public async Task<bool> HasPermission(HasPermissionDTO hasPermission)
        {
            var result = await _rolePermissionRepository.HasPermission(hasPermission);
            return result;
        }
    }
}
