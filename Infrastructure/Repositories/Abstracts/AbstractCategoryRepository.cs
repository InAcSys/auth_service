using AuthService.Domain.DTOs.Categories;
using AuthService.Domain.Entities.Concretes;
using AuthService.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Repositories.Abstracts
{
    public class AbstractCategoryRepository(
        DbContext context,
        IPermissionRepository permissionRepository
    ) : Repository<Category, int>(context), ICategoryRepository
    {
        private readonly IPermissionRepository _permissionRepository = permissionRepository;

        public Task<CategoryDTO> GetPermissions(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
