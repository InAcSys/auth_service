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
            { 1, new List<int> { 3, 6, 10, 15, 21, 24, 28, 33, 43, 44, 46, 47, 52 } }, // student
            { 2, new List<int> { 3, 6, 7, 8, 9, 10, 11, 14, 15, 16, 17, 20, 21, 22, 23, 24, 25, 26, 27, 28, 33, 40, 41, 42, 46, 47 } }, // teacher
            { 3, new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54 } }, // admin
            { 4, new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54 } }, // principal
            { 5, new List<int> { 3, 6, 10, 28, 33, 37, 38, 39, 45, 46, 47 } } // parent
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
