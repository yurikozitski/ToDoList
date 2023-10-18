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
		public string? Email { get; set; }
		public string? Password { get; set; }
	}
}
