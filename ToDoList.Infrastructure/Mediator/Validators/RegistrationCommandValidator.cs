using ToDoList.Infrastructure.Mediator.Commands;
using FluentValidation;

namespace ToDoList.Infrastructure.Mediator.Validators
{
    public class RegistrationCommandValidator : AbstractValidator<RegistrationCommand>
	{
		public RegistrationCommandValidator()
		{
			RuleFor(x => x.FirstName).NotEmpty().Length(2,20);
			RuleFor(x => x.LastName).NotEmpty().Length(2,20);
			RuleFor(x => x.Email).EmailAddress().NotEmpty();
			RuleFor(x => x.Password).NotEmpty().Length(8,50);
		}
	}
}
