using MediatR;
using ToDoList.Core.RepositoryInterfaces;

namespace ToDoList.Infrastructure.Mediator.Commands
{
    public class AddUserTaskPlannedTimeHandler : IRequestHandler<AddUserTaskPlannedTimeCommand>
	{
		private readonly ITaskRepository taskRepository;

		public AddUserTaskPlannedTimeHandler(ITaskRepository _taskRepository)
		{
			taskRepository = _taskRepository;
		}

		public async Task Handle(AddUserTaskPlannedTimeCommand addUserTaskPlannedTimeCommand, CancellationToken cancellationToken)
		{
			await taskRepository.AddUserTaskPlannedTimeAsync(addUserTaskPlannedTimeCommand.UserTaskId, addUserTaskPlannedTimeCommand.Time);
		}
	}
}
