using Entities.Dto.AppUser;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.AppUser
{
    public class AppUserLoginInputValidator : AbstractValidator<AppUserLoginInput>
    {
        public AppUserLoginInputValidator()
        {
            RuleFor(t => t.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(t => t.Password).NotNull().NotEmpty();
        }
    }
}
