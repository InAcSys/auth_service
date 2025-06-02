using AuthService.Domain.DTOs.Categories;
using AuthService.Domain.Entities.Concretes;

namespace AuthService.Application.Services.Interfaces
{
    public interface IPermissionService : IService<Permission, int>
    {
        Task<IEnumerable<PermissionsByCategoryDTO>> GetPermissionsByRole(int roleId, Guid tenantId);
    }
}
