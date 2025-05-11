using AuthService.Domain.DTOs.Categories;
using AuthService.Domain.Entities.Concretes;

namespace AuthService.Infrastructure.Repositories.Interfaces
{
    public interface ICategoryRepository : IRepository<Category, int>
    {
        Task<CategoryDTO> GetPermissions(int categoryId);
        Task<CategoryDTO> GetPermissionsByRole(int categoryId, int roleId, Guid tenantId);
    }
}
