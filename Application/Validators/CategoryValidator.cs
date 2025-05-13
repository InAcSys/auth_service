using AuthService.Domain.Entities.Concretes;
using FluentValidation;

namespace AuthService.Application.Validators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(category => category.Name)
                .NotEmpty()
                .WithMessage("Category name can not be empty")
                .Length(3, 100)
                .WithMessage("Category name length must be between 3 to 100");
        }
    }
}
