using AuthService.Application.Services.Concretes;
using AuthService.Application.Services.Interfaces;
using AuthService.Application.Validators;
using AuthService.Domain.Entities.Concretes;
using AuthService.Infrastructure.Context;
using AuthService.Infrastructure.Context.Concretes;
using AuthService.Infrastructure.Repositories.Concretes;
using AuthService.Infrastructure.Repositories.Interfaces;
using AuthService.Presentation.Profiles;
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
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IRolePermissionService, RolePermissionService>();
            services.AddScoped<IService<Category, int>, CategoryService>();
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IRepository<Role, int>, RoleRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddValidatorsFromAssemblyContaining<RoleValidator>();
            services.AddValidatorsFromAssemblyContaining<PermissionValidator>();
            services.AddValidatorsFromAssemblyContaining<RolePermissionValidator>();
            services.AddValidatorsFromAssemblyContaining<CategoryValidator>();

            services.AddAutoMapper(typeof(CategoryProfile));
            services.AddAutoMapper(typeof(RoleProfile));
            services.AddAutoMapper(typeof(PermissionProfile));
            services.AddAutoMapper(typeof(RolePermissionProfile));

            return services;
        }
    }
}
