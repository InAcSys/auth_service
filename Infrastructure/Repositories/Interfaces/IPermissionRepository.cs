using AuthService.Domain.DTOs.RolePermission;
using AuthService.Domain.Entities.Concretes;

namespace AuthService.Infrastructure.Repositories.Interfaces
{
    public interface IPermissionRepository : IRepository<Permission, int>
    {
        Task<IEnumerable<PermissionDTO>> GetPermissionsByCategoryId(int categoryId);
    }
}
