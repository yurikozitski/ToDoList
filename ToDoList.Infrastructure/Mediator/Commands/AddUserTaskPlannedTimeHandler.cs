using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Core.RepositoryInterfaces;

namespace ToDoList.Infrastructure.Mediator.Commands
{
	public class AddUserTaskPlannedTimeHandler : IRequestHandler<AddUserTaskPlannedTimeCommand>
	{
		private readonly ITaskRepository taskRepository;
		private readonly IUserRepository userRepository;

		public AddUserTaskPlannedTimeHandler(ITaskRepository _taskRepository, IUserRepository _userRepository)
		{
			taskRepository = _taskRepository;
			userRepository = _userRepository;
		}

		public async Task Handle(AddUserTaskPlannedTimeCommand addUserTaskPlannedTimeCommand, CancellationToken cancellationToken)
		{
			await taskRepository.AddUserTaskPlannedTimeAsync(addUserTaskPlannedTimeCommand.UserTaskId,addUserTaskPlannedTimeCommand.Time);

		}
	}
}
