using MediatR;
using ToDoList.Core.Models;

namespace ToDoList.Infrastructure.Mediator.Queries
{
    public class GetTaskListsQuery:IRequest<List<TaskList>>
	{
		public string UserEmail { get; set; } = default!;
	}
}
