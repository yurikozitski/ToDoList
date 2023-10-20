using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Core.Models;

namespace ToDoList.Infrastructure.Mediator.Queries
{
	public class GetAllUserTasksQuery: IRequest<List<UserTask>>
	{
		public string? UserEmail { get; set; }
	}
}

