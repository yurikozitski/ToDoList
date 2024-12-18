using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Core.Models;

namespace ToDoList.Infrastructure.Mediator.Queries
{
	public class GetUserTasksByTaskListQuery : IRequest<List<UserTask>>
	{
		public Guid TaskListId { get; set; }
	}
}
