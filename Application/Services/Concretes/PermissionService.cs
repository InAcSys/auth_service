using AuthService.Application.Services.Abstracts;
using AuthService.Application.Services.Interfaces;
using AuthService.Domain.Entities.Concretes;
using AuthService.Infrastructure.Repositories.Interfaces;
using FluentValidation;
namespace AuthService.Application.Services.Concretes
{
    public class PermissionService(
        IRolePermissionRepository rolePermissionRepository,
        ICategoryRepository categoryRepository,
        IPermissionRepository permissionRepository,
        IValidator<Permission> validator
    ) : AbstractPermissionService(rolePermissionRepository, categoryRepository, permissionRepository, validator)
    {
    }
}
