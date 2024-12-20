using MediatR;

namespace ToDoList.Infrastructure.Mediator.Commands
{
    public class MarkUserTaskAsImportantCommand : IRequest
	{
		public Guid UserTaskId { get; set; }
	}
}
