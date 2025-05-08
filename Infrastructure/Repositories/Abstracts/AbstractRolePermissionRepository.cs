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
                    var rolePermissionId = await Exists(id, permission);
                    Console.WriteLine(rolePermissionId);
                    if (rolePermissionId == 0)
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
                    else
                    {
                        var entity = await GetById(rolePermissionId);
                        if (entity is not null)
                        {
                            entity.IsActive = true;
                            await Update(rolePermissionId, entity);
                        }
                    }
                }
            }

            var result = await GetPermissionsByRoleId(id);
            return result;
        }

        public async Task<int> Exists(int roleId, int permissionId)
        {
            var role = await _roleRepository.GetById(roleId) is not null;
            var permission = await _permissionRepository.GetById(permissionId) is not null;
            if (role && permission)
            {
                int id = await _context
                    .Set<RolePermission>()
                    .Where(rp => rp.RoleId == roleId && rp.PermissionId == permissionId)
                    .Select(rp => rp.Id)
                    .FirstOrDefaultAsync();
                return id;
            }
            return 0;
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
                .Where(rp => rp.RoleId == roleId && rp.IsActive)
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

        public async Task<bool> RevokePermissions(int id, PermissionsDTO permissions)
        {
            var role = await _roleRepository.GetById(id);
            if (role is null)
            {
                throw new InvalidOperationException($"Role with id {id} not found");
            }

            foreach (var permissionId in permissions.permissionsId)
            {
                int entityId = await _context
                    .Set<RolePermission>()
                    .Where(rp => rp.RoleId == id && rp.PermissionId == permissionId)
                    .Select(rp => rp.Id)
                    .FirstOrDefaultAsync();

                if (entityId != 0)
                {
                    var rolePermission = await GetById(entityId);
                    if (rolePermission is not null)
                    {
                        rolePermission.IsActive = false;
                        rolePermission.Deleted = DateTime.UtcNow;
                        await Update(entityId, rolePermission);
                    }
                }
            }
            return true;
        }

        public async Task<bool> HasPermission(HasPermissionDTO hasPermission)
        {
            int id = await Exists(hasPermission.RoleId, hasPermission.PermissionId);
            return id != 0;
        }
    }
}
