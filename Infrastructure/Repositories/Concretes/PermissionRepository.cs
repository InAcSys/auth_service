using AuthService.Domain.Entities.Concretes;
using AuthService.Infrastructure.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Context.Concretes
{
    public class PermissionRepository(DbContext context) : Repository<Permission, int>(context)
    {
    }
}
