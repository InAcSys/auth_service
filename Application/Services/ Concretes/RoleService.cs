using AuthService.Application.Services.Abstracts;
using AuthService.Domain.Entities.Concretes;
using AuthService.Infrastructure.Repositories.Interfaces;
using FluentValidation;

namespace AuthService.Application.Services.Concretes
{
    public class RoleService(IValidator<Role> validator, IRepository<Role, int> repository) : Service<Role, int>(validator, repository)
    {
    }
}
