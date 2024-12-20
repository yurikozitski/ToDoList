using Azure.Core;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using ToDoList.Infrastructure.DTOs;
using ToDoList.Infrastructure.Mediator.Commands;

namespace webapi.Controllers
{
    [Route("[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IMediator mediator;
        private readonly IValidator<LoginCommand> loginValidator;
        private readonly IValidator<RegistrationCommand> registerValidator;

        public AccountController(IMediator _mediator,
			IValidator<LoginCommand> _commandValidator,
            IValidator<RegistrationCommand> _registerValidator)
        {
            mediator = _mediator;
            loginValidator = _commandValidator;
            registerValidator = _registerValidator;
        }

        [HttpPost("Register")]
		public async Task<ActionResult<UserDto>> RegisterAsync()
		{
			var formCollection = await Request.ReadFormAsync();
			IFormFile? image = default;

			if (formCollection.Files.Count != 0)
            {
                image = formCollection.Files[0];
            }

            RegistrationCommand registrationCommand = new()
			{
				FirstName = formCollection["FirstName"]!,
				LastName = formCollection["LastName"]!,
				Email = formCollection["Email"]!,
				Image = image,
				Password= formCollection["Password"]!
			};

            var validationResult = await registerValidator.ValidateAsync(registrationCommand);

            if (!validationResult.IsValid)
            {
                var mesSb = new StringBuilder();

                foreach (var error in validationResult.Errors)
                {
                    mesSb.Append($"{error.PropertyName} : {error.ErrorMessage}; ");
                }

                return BadRequest(mesSb.ToString());
            }

            return await mediator.Send(registrationCommand);
		}

		[HttpPost("Login")]
		public async Task<ActionResult<UserDto>> LoginAsync(LoginCommand loginCommand)
		{
            var validationResult = await loginValidator.ValidateAsync(loginCommand);

            if (!validationResult.IsValid)
            {
                var mesSb = new StringBuilder();

                foreach (var error in validationResult.Errors)
                {
                    mesSb.Append($"{error.PropertyName} : {error.ErrorMessage}; ");
                }

                return BadRequest(mesSb.ToString());
            }

            return await mediator.Send(loginCommand);
		}
	}	
}
