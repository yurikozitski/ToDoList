using MediatR;

namespace ToDoList.Infrastructure.Mediator.Commands
{
    public class AddTaskListCommand : IRequest
	{
		public string TaskListName { get; set; } = default!;

		public string UserEmail { get; set; } = default!;
	}
}
