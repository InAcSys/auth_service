using AuthService.Application.Services.Abstracts;
using AuthService.Infrastructure.Repositories.Interfaces;

namespace AuthService.Application.Services.Concretes
{
    public class TenantService
    (
        IRolePermissionRepository repository
    ) : AbstractTenantService(repository)
    {
    }
}
