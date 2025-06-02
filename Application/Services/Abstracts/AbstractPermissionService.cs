using AuthService.Application.Services.Interfaces;
using AuthService.Domain.DTOs.Categories;
using AuthService.Domain.DTOs.Permissions;
using AuthService.Domain.Entities.Concretes;
using AuthService.Infrastructure.Repositories.Interfaces;
using FluentValidation;

namespace AuthService.Application.Services.Abstracts
{
    public abstract class AbstractPermissionService(
        IRolePermissionRepository rolePermissionRepository,
        ICategoryRepository categoryRepository,
        IPermissionRepository permissionRepository,
        IValidator<Permission> validator
    ) : Service<Permission, int>(validator, permissionRepository), IPermissionService
    {

        private readonly IRolePermissionRepository _rolePermissionRepository = rolePermissionRepository;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IPermissionRepository _permissionRepository = permissionRepository;

        public async Task<IEnumerable<PermissionsByCategoryDTO>> GetPermissionsByRole(int roleId, Guid tenantId)
        {
            if (roleId <= 0)
                throw new ArgumentException("The role id is invalid for this operation");

            if (tenantId == Guid.Empty)
                throw new ArgumentNullException(nameof(tenantId), "This tenant ID is invalid for this operation");

            var rolePermissions = await _rolePermissionRepository.GetPermissionsByRoleId(roleId, tenantId);

            if (rolePermissions?.Permissions == null || !rolePermissions.Permissions.Any())
                throw new InvalidOperationException("This role does not have permissions");

            var allPermissions = await _permissionRepository.GetAll(tenantId);

            var permittedIds = rolePermissions.Permissions.Select(p => p.Id).ToHashSet();

            var permittedPermissions = allPermissions
                .Where(p => permittedIds.Contains(p.Id))
                .ToList();

            var allCategories = await _categoryRepository.GetAll(tenantId);

            var categoryDict = allCategories.ToDictionary(c => c.Id);

            var permissionsByCategory = permittedPermissions
                .GroupBy(p => p.CategoryId)
                .ToDictionary(g => g.Key, g => g.Select(p => new PermissionDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Path = p.Path,
                    Code = p.Code
                }).ToList());

            List<PermissionsByCategoryDTO> BuildCategoryTree(int? parentId)
            {
                return allCategories
                    .Where(c => c.ParentId == parentId)
                    .Select(c => new PermissionsByCategoryDTO
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Path = c.Path,
                        Permissions = permissionsByCategory.ContainsKey(c.Id) ? permissionsByCategory[c.Id] : new List<PermissionDTO>(),
                        SubCategories = BuildCategoryTree(c.Id)
                    })
                    .Where(c => c.Permissions.Any() || c.SubCategories.Any())
                    .ToList();
            }

            var result = BuildCategoryTree(null);

            return result;
        }
    }

}
