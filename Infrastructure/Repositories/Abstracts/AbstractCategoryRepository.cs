using AuthService.Domain.DTOs.Categories;
using AuthService.Domain.Entities.Concretes;
using AuthService.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Repositories.Abstracts
{
    public class AbstractCategoryRepository(
        DbContext context,
        IPermissionRepository permissionRepository,
        IRolePermissionRepository rolePermissionRepository
    ) : Repository<Category, int>(context), ICategoryRepository
    {
        private readonly IPermissionRepository _permissionRepository = permissionRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository = rolePermissionRepository;

        public Task<CategoryDTO> GetPermissions(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDTO> GetPermissionsByRole(int categoryId, int roleId, Guid tenantId)
        {
            throw new NotImplementedException();
        }
    }
}
