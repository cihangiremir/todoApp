using Entities.Dto.Todo;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.AppUser
{
    public class TodoUpdateInputValidator : AbstractValidator<TodoUpdateInput>
    {
        public TodoUpdateInputValidator()
        {
            RuleFor(t => t.UserId).NotNull().NotEmpty();
        }
    }
}
