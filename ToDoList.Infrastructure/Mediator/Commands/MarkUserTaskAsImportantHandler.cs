using MediatR;
using ToDoList.Core.RepositoryInterfaces;

namespace ToDoList.Infrastructure.Mediator.Commands
{
    public class MarkUserTaskAsImportantHandler : IRequestHandler<MarkUserTaskAsImportantCommand>
	{
		private readonly ITaskRepository taskRepository;

		public MarkUserTaskAsImportantHandler(ITaskRepository _taskRepository)
		{
			taskRepository = _taskRepository;
		}

		public async Task Handle(MarkUserTaskAsImportantCommand markUserTaskAsImportantCommand, CancellationToken cancellationToken)
		{
			await taskRepository.MarkUserTaskAsImportantAsync(markUserTaskAsImportantCommand.UserTaskId);
		}
	}
}
