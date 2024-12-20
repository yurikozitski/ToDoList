using MediatR;
using ToDoList.Infrastructure.DTOs;

namespace ToDoList.Infrastructure.Mediator.Commands
{
    public class LoginCommand : IRequest<UserDto>
	{
		public string Email { get; set; } = default!;

		public string Password { get; set; } = default!;
	}
}
