using FluentValidation;
using ToDoList.Infrastructure.Mediator.Commands;

namespace ToDoList.Infrastructure.Mediator.Validators
{
    public class TokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public TokenCommandValidator()
        {
            RuleFor(x => x.Token).NotEmpty();
            RuleFor(x => x.RefreshToken).NotEmpty();
        }
    }
}
