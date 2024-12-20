using MediatR;

namespace ToDoList.Infrastructure.Mediator.Commands
{
    public class AddUserTaskPlannedTimeCommand:IRequest
	{
		public DateTime Time { get; set; }

		public Guid UserTaskId { get; set; }
	}
}
