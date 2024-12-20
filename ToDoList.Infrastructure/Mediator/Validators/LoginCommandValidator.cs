using FluentValidation;
using ToDoList.Infrastructure.Mediator.Commands;

namespace ToDoList.Infrastructure.Mediator.Validators
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
	{
		public LoginCommandValidator()
		{
			RuleFor(x => x.Email).EmailAddress().NotEmpty();
			RuleFor(x => x.Password).NotEmpty().Length(8, 50);
		}
	}
}
