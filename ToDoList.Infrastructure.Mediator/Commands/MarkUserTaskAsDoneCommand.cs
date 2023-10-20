using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Infrastructure.Mediator.Commands
{
	public class MarkUserTaskAsDoneCommand:IRequest
	{
		public Guid UserTaskId { get; set; }	
	}
}
