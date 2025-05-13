using AuthService.Application.Services.Abstracts;
using AuthService.Domain.Entities.Concretes;
using AuthService.Infrastructure.Repositories.Interfaces;
using FluentValidation;

namespace AuthService.Application.Services.Concretes
{
    public class RolePermissionService(
        IValidator<RolePermission> validator,
        IRepository<RolePermission, int> repository,
        IRolePermissionRepository rolePermissionRepository
    ) : AbstractRolePermissionService(validator, repository, rolePermissionRepository)
    {
    }
}
