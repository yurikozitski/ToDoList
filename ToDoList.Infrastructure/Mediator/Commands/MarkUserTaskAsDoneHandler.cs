using MediatR;
using ToDoList.Core.RepositoryInterfaces;

namespace ToDoList.Infrastructure.Mediator.Commands
{
    public class MarkUserTaskAsDoneHandler : IRequestHandler<MarkUserTaskAsDoneCommand>
	{
		private readonly ITaskRepository taskRepository;

		public MarkUserTaskAsDoneHandler(ITaskRepository _taskRepository)
		{
			taskRepository = _taskRepository;
		}

		public async Task Handle(MarkUserTaskAsDoneCommand markUserTaskAsDoneCommand, CancellationToken cancellationToken)
		{
			await taskRepository.MarkUserTaskAsDoneAsync(markUserTaskAsDoneCommand.UserTaskId);
		}
	}
}
