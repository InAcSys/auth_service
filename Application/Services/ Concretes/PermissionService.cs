using AuthService.Application.Services.Abstracts;
using AuthService.Domain.Entities.Concretes;
using AuthService.Infrastructure.Repositories.Interfaces;
using FluentValidation;
namespace AuthService.Application.Services.Concretes
{
    public class PermissionService(IValidator<Permission> validator, IRepository<Permission, int> repository) : Service<Permission, int>(validator, repository)
    {
    }
}
