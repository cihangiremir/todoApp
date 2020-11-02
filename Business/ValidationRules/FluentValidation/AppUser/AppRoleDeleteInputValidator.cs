using Entities.Dto.AppRole;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.AppUser
{
    public class AppRoleDeleteInputValidator : AbstractValidator<AppRoleDeleteInput>
    {
        public AppRoleDeleteInputValidator()
        {
            RuleFor(t => t.Id).NotNull().NotEmpty();
            RuleFor(t => t.Name).NotNull().NotEmpty();
        }
    }
}
