using AuthService.Application.Services.Abstracts;
using AuthService.Domain.Entities.Concretes;
using AuthService.Infrastructure.Repositories.Interfaces;
using FluentValidation;

namespace AuthService.Application.Services.Concretes
{
    public class CategoryService(
        IRepository<Category, int> repository,
        IValidator<Category> validator
    ) : Service<Category, int>(validator, repository)
    {
    }
}
