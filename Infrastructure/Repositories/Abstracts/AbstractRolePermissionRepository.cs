using AuthService.Domain.DTOs.RolePermission;
using AuthService.Domain.Entities.Concretes;
using AuthService.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Repositories.Abstracts
{
    public abstract class AbstractRolePermissionRepository(
        DbContext context,
        IRepository<Role, int> roleRepository,
        IRepository<Permission, int> permissionRepository
    ) : Repository<RolePermission, int>(context), IRolePermissionRepository
    {
        protected IRepository<Role, int> _roleRepository = roleRepository;
        protected IRepository<Permission, int> _permissionRepository = permissionRepository;

        public async Task<RolePermissionsDTO> Assign(int id, CreateRolePermissionDTO permissions)
        {
            var roleExist = await _roleRepository.GetById(id) is null;
            if (roleExist)
            {
                throw new InvalidOperationException($"Role with id {id} not found");
            }

            foreach (var permission in permissions.Permissions)
            {
                var permissionExist = await _permissionRepository.GetById(permission) is not null;
                if (permissionExist)
                {
                    var rolePermissionExist = await Exists(id, permission);
                    if (!rolePermissionExist)
                    {
                        var total = await _context.Set<RolePermission>().ToListAsync();
                        var rolePermission = new RolePermission
                        {
                            Id = total.Count + 1,
                            RoleId = id,
                            PermissionId = permission
                        };
                        await Create(rolePermission);
                    }
                }
            }

            var result = await GetPermissionsByRoleId(id);
            return result;
        }

        public async Task<bool> Exists(int roleId, int permissionId)
        {
            var role = await _roleRepository.GetById(roleId) is not null;
            var permission = await _permissionRepository.GetById(permissionId) is not null;
            if (role && permission)
            {
                return await _context
                    .Set<RolePermission>()
                    .AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);
            }
            return false;
        }

        public async Task<RolePermissionsDTO> GetPermissionsByRoleId(int roleId)
        {
            var role = await _roleRepository.GetById(roleId);
            if (role is null)
            {
                throw new InvalidOperationException("Role not found");
            }

            var permissionsId = await _context
                .Set<RolePermission>()
                .Where(rp => rp.RoleId == roleId)
                .Select(rp => rp.PermissionId)
                .ToListAsync();

            var permissions = new List<PermissionDTO>();
            foreach (var id in permissionsId)
            {
                var permission = await _permissionRepository.GetById(id);
                if (permission is not null)
                {
                    permissions.Add(new PermissionDTO
                    {
                        Id = permission.Id,
                        Name = permission.Name,
                        Description = permission.Description,
                        Path = permission.Path
                    });
                }
            }

            var roleDto = new RoleDTO
            {
                Id = role.Id,
                Name = role.Name
            };

            var result = new RolePermissionsDTO
            {
                Role = roleDto,
                Permissions = permissions
            };

            return result;
        }

        public Task<bool> HasPermission(HasPermissionDTO hasPermission)
        {
            return Exists(hasPermission.RoleId, hasPermission.PermissionId);
        }
    }
}
