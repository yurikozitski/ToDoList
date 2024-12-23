using MediatR;

namespace ToDoList.Infrastructure.Mediator.Commands
{
    public class MarkUserTaskAsDoneCommand : IRequest
	{
		public Guid UserTaskId { get; set; }	
	}
}
