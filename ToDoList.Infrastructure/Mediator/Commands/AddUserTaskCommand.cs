using MediatR;

namespace ToDoList.Infrastructure.Mediator.Commands
{
    public class AddUserTaskCommand : IRequest
	{
		public string Text { get; set; } = default!;

		public Guid TaskListId { get; set; }
	}
}
