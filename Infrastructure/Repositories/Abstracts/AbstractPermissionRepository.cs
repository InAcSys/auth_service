using AuthService.Domain.DTOs.Permissions;
using AuthService.Domain.Entities.Concretes;
using AuthService.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Repositories.Abstracts
{
    public abstract class AbstractPermissionRepository
    (
        DbContext context,
        IRepository<Category, int> categoryRepository
    ) : Repository<Permission, int>(context), IPermissionRepository
    {
        private readonly IRepository<Category, int> _categoryRepository = categoryRepository;
        public async Task<IEnumerable<PermissionDTO>> GetPermissionsByCategoryId(int categoryId, Guid tenantId)
        {
            var notExists = await _categoryRepository.GetById(categoryId, tenantId) is null;

            if (notExists)
            {
                throw new InvalidOperationException("Category not found");
            }

            var permissions = await _context
                .Set<Permission>()
                .Where(permission => permission.CategoryId == categoryId)
                .ToListAsync();

            var permissionsByCategory = new List<PermissionDTO>();

            foreach (var permission in permissions)
            {
                var permissionDTO = new PermissionDTO
                {
                    Id = permission.Id,
                    Name = permission.Name,
                    Description = permission.Description,
                    Path = permission.Path
                };
                permissionsByCategory.Add(permissionDTO);
            }

            return permissionsByCategory;
        }
    }
}
