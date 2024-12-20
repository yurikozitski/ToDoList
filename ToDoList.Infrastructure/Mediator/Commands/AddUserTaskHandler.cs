using MediatR;
using System.Net;
using ToDoList.Core.RepositoryInterfaces;
using ToDoList.Infrastructure.Exeptions;

namespace ToDoList.Infrastructure.Mediator.Commands
{
    public class AddUserTaskHandler : IRequestHandler<AddUserTaskCommand>
	{
		private readonly ITaskRepository taskRepository;

		public AddUserTaskHandler(ITaskRepository _taskRepository)
		{
			taskRepository = _taskRepository;
		}

		public async Task Handle(AddUserTaskCommand addUserTaskCommand, CancellationToken cancellationToken)
		{			
			await taskRepository.AddUserTaskAsync(addUserTaskCommand.TaskListId, addUserTaskCommand.Text);			
		}
	}
}
