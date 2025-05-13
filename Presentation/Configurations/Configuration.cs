using AuthService.Application.Decoders.JWT;
using AuthService.Application.Services.Concretes;
using AuthService.Application.Services.Interfaces;
using AuthService.Application.Validators;
using AuthService.Domain.Entities.Concretes;
using AuthService.Infrastructure.Context;
using AuthService.Infrastructure.Context.Concretes;
using AuthService.Infrastructure.Repositories.Concretes;
using AuthService.Infrastructure.Repositories.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Presentation.Configurations
{
    public static class Configuration
    {
        public static IServiceCollection AddConfiguration(this IServiceCollection services)
        {
            var connection = Environment.GetEnvironmentVariable("AUTHORIZATION_SERVICE_DATABASE_STRING_CONNECTION");
            if (connection == null)
            {
                throw new ArgumentException("Connection not found");
            }

            services.AddDbContext<DbContext, AuthServiceDbContext>(
                options => options.UseMySql
                (
                    connection,
                    new MySqlServerVersion(
                        new Version(8, 0, 23)
                    ),
                    mysqlOptions =>
                    {
                        mysqlOptions.EnableRetryOnFailure();
                    }
                )
            );

            services.AddScoped<IService<Role, int>, RoleService>();
            services.AddScoped<IService<Permission, int>, PermissionService>();
            services.AddScoped<IService<RolePermission, int>, RolePermissionService>();
            services.AddScoped<IRolePermissionService, RolePermissionService>();
            services.AddScoped<IService<Category, int>, CategoryService>();
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IRepository<Role, int>, RoleRepository>();
            services.AddScoped<IRepository<Permission, int>, PermissionRepository>();
            services.AddScoped<IRepository<RolePermission, int>, RolePermissionRepository>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            services.AddScoped<IRepository<Category, int>, CategoryRepository>();
            services.AddScoped<IJWTDecoder, JWTDecoder>();

            services.AddValidatorsFromAssemblyContaining<RoleValidator>();
            services.AddValidatorsFromAssemblyContaining<PermissionValidator>();
            services.AddValidatorsFromAssemblyContaining<RolePermissionValidator>();
            services.AddValidatorsFromAssemblyContaining<CategoryValidator>();

            return services;
        }
    }
}
