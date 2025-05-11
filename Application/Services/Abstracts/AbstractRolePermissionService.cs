using AuthService.Application.Decoders.JWT;
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
        IRolePermissionRepository rolePermissionRepository,
        IJWTDecoder decoder
    ) : Service<RolePermission, int>(validator, repository), IRolePermissionService
    {
        private readonly IRolePermissionRepository _rolePermissionRepository = rolePermissionRepository;
        private readonly IJWTDecoder _decoder = decoder;
        public async Task<RolePermissionsDTO> GetPermissionsByRoleId(int roleId, Guid tenantId)
        {
            var result = await _rolePermissionRepository.GetPermissionsByRoleId(roleId, tenantId);
            return result;
        }

        public async Task<RolePermissionsDTO> Assign(int id, CreateRolePermissionDTO permissions, Guid tenantId)
        {
            var result = await _rolePermissionRepository.Assign(id, permissions, tenantId);
            return result;
        }

        public async Task<bool> HasPermission(HasPermissionDTO hasPermission)
        {
            var jwt = _decoder.Decoder(hasPermission.JWT);
            var permission = new VerifyPermissionDTO
            {
                RoleId = jwt.RoleId,
                PermissionId = hasPermission.PermissionId
            };
            var result = await _rolePermissionRepository.HasPermission(permission);
            return result;
        }

        public async Task<bool> RevokePermissions(int id, PermissionsDTO permissions, Guid tenantId)
        {
            var result = await _rolePermissionRepository.RevokePermissions(id, permissions, tenantId);
            return result;
        }
    }
}
