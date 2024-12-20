using MediatR;
using ToDoList.Core.Models;

namespace ToDoList.Infrastructure.Mediator.Queries
{
    public class GetUserTasksByTaskListQuery : IRequest<List<UserTask>>
	{
		public Guid TaskListId { get; set; }
	}
}
