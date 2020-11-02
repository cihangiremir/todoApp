using Entities.Dto.AppRole;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.AppUser
{
    public class AppRoleAddInputValidator : AbstractValidator<AppRoleAddInput>
    {
        public AppRoleAddInputValidator()
        {
            RuleFor(t => t.Name).NotNull().NotEmpty();
        }
    }
}
