using AuthService.Domain.Entities.Concretes;
using FluentValidation;

namespace AuthService.Application.Validators
{
    public class RoleValidator : AbstractValidator<Role>
    {
        public RoleValidator()
        {
            RuleFor(role => role.Name).NotEmpty().WithMessage("Role name must not be empty").Length(3, 100).WithMessage("Role name length must be between 3 and 100");
        }
    }
}
