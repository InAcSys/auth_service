using AuthService.Domain.Entities.Concretes;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Context
{
    public class AuthServiceDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
