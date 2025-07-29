using AuthService.Application.Services.Interfaces;
using AuthService.Domain.DTOs.RolePermission;
using AuthService.Infrastructure.Repositories.Interfaces;

namespace AuthService.Application.Services.Abstracts
{
    public abstract class AbstractTenantService(
        IRolePermissionRepository repository
    ) : ITenantService
    {
        private readonly IRolePermissionRepository _repository = repository;
        private readonly Dictionary<int, List<int>> _permissionsToCategory = new Dictionary<int, List<int>>
        {
            { 1, new List<int> { 3, 5, 11 } }, // student
            { 2, new List<int> { 2, 3, 4, 5, 12 } }, // teacher
            { 3, new List<int> { 1, 3, 4, 6, 7, 8, 10 } }, // admin
            { 4, new List<int> { 1, 3, 4, 6, 7, 8, 9, 10 } }, // principal
            { 5, new List<int> {  } } // parent
        };


        public async Task<bool> Initialize(Guid id)
        {
            try
            {
                foreach (var item in _permissionsToCategory)
                {
                    var permissions = new CreateRolePermissionDTO
                    {
                        Permissions = item.Value
                    };
                    await _repository.Assign(item.Key, permissions, id);
                }
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
    }
}
