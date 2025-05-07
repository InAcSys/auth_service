using AuthService.Domain.Entities.Concretes;
using AuthService.Infrastructure.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Repositories.Concretes
{
    public class RoleRepository(DbContext context) : Repository<Role, int>(context)
    {
    }
}
