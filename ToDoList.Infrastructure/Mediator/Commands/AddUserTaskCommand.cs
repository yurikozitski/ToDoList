using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Infrastructure.Mediator.Commands
{
	public class AddUserTaskCommand : IRequest
	{
		public string? Text { get; set; }
		public Guid TaskListId { get; set; }
	}
}
