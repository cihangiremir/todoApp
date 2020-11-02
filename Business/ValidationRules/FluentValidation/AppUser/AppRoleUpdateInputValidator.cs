using Entities.Dto.AppRole;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.AppUser
{
    public class AppRoleUpdateInputValidator : AbstractValidator<AppRoleUpdateInput>
    {
        public AppRoleUpdateInputValidator()
        {
            RuleFor(t => t.Id).NotNull().NotEmpty();
            RuleFor(t => t.Name).NotNull().NotEmpty();
        }
    }
}
