using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Infrastructure.DTOs;

namespace ToDoList.Infrastructure.Mediator.Commands
{
	public class LoginCommand : IRequest<UserDTO>
	{
		public string Email { get; set; } = default!;
		public string Password { get; set; } = default!;
	}
}
