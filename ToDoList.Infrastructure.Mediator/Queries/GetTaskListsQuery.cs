using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ToDoList.Core.Models;

namespace ToDoList.Infrastructure.Mediator.Queries
{
	public class GetTaskListsQuery:IRequest<List<TaskList>>
	{
		public string? UserEmail { get; set; }
	}
}
