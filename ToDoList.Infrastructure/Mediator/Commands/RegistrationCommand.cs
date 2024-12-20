using MediatR;
using Microsoft.AspNetCore.Http;
using ToDoList.Infrastructure.DTOs;

namespace ToDoList.Infrastructure.Mediator.Commands
{
    public class RegistrationCommand : IRequest<UserDto>
	{
		public string FirstName { get; set; } = default!;

		public string LastName { get; set; } = default!;

		public string Email { get; set; } = default!;

		public IFormFile? Image { get; set; }

		public string Password { get; set; } = default!;
	}
}
