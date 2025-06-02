using AuthService.Domain.Entities.Concretes;
using AuthService.Infrastructure.Repositories.Abstracts;
using AuthService.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Context.Concretes
{
    public class PermissionRepository(DbContext context, ICategoryRepository categoryRepository) : AbstractPermissionRepository(context, categoryRepository)
    {
    }
}
