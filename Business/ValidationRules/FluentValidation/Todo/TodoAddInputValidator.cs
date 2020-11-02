using Entities.Dto.Todo;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.AppUser
{
    public class TodoAddInputValidator : AbstractValidator<TodoAddInput>
    {
        public TodoAddInputValidator()
        {
            RuleFor(t => t.UserId).NotNull().NotEmpty();
        }
    }
}
