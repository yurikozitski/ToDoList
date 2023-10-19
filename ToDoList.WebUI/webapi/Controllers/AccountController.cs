using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Infrastructure.DTOs;
using ToDoList.Infrastructure.Mediator.Commands;

namespace webapi.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IMediator mediator;

		public AccountController(IMediator _mediator)
		{
			mediator = _mediator;
		}

		[HttpPost("Register")]
		public async Task<ActionResult<UserDTO>> RegisterAsync(RegistrationCommand registrationCommand)
		{
			return await mediator.Send(registrationCommand);
		}

		[HttpPost("Login")]
		public async Task<ActionResult<UserDTO>> LoginAsync(LoginCommand loginCommand)
		{
			return await mediator.Send(loginCommand);
		}

	}	
	

}
