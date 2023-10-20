using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Core.RepositoryInterfaces;

namespace ToDoList.Infrastructure.Mediator.Commands
{
	public class AddUserTaskHandler : IRequestHandler<AddUserTaskCommand>
	{
		private readonly ITaskRepository taskRepository;
		private readonly IUserRepository userRepository;

		public AddUserTaskHandler(ITaskRepository _taskRepository, IUserRepository _userRepository)
		{
			taskRepository = _taskRepository;
			userRepository = _userRepository;
		}

		public async Task Handle(AddUserTaskCommand addUserTaskCommand, CancellationToken cancellationToken)
		{
			await taskRepository.AddUserTaskAsync(addUserTaskCommand.TaskListId, addUserTaskCommand.Text);

		}
	}
}
