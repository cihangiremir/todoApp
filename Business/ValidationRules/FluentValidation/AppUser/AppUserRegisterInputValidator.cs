using Entities.Dto.AppUser;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.AppUser
{
    public class AppUserRegisterInputValidator : AbstractValidator<AppUserRegisterInput>
    {
        public AppUserRegisterInputValidator()
        {
            RuleFor(t => t.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(t => t.Password).NotNull().NotEmpty();
        }
    }
}
