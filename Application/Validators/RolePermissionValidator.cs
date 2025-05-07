using AuthService.Domain.Entities.Concretes;
using FluentValidation;

namespace AuthService.Application.Validators
{
    public class RolePermissionValidator : AbstractValidator<RolePermission>
    {
        public RolePermissionValidator()
        { }
    }
}
