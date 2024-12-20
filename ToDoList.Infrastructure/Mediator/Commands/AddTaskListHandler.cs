using MediatR;
using ToDoList.Core.RepositoryInterfaces;

namespace ToDoList.Infrastructure.Mediator.Commands
{
    public class AddTaskListHandler : IRequestHandler<AddTaskListCommand>
	{
		private readonly ITaskRepository taskRepository;
		private readonly IUserRepository userRepository;

		public AddTaskListHandler(ITaskRepository _taskRepository, IUserRepository _userRepository)
		{
			taskRepository = _taskRepository;
			userRepository = _userRepository;
		}

		public async Task Handle(AddTaskListCommand addTaskListCommand, CancellationToken cancellationToken)
		{
			var user = await userRepository.GetByEmailAsync(addTaskListCommand.UserEmail);

			await taskRepository.AddTaskListAsync(addTaskListCommand.TaskListName, user!);
		}
	}
}
