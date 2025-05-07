using AuthService.Domain.Entities.Concretes;
using FluentValidation;

namespace AuthService.Application.Validators
{
    public class PermissionValidator : AbstractValidator<Permission>
    {
        public PermissionValidator()
        { }
    }
}
