using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Core.RepositoryInterfaces;

namespace ToDoList.Infrastructure.Mediator.Commands
{
	public class MarkUserTaskAsDoneHandler : IRequestHandler<MarkUserTaskAsDoneCommand>
	{
		private readonly ITaskRepository taskRepository;
		private readonly IUserRepository userRepository;

		public MarkUserTaskAsDoneHandler(ITaskRepository _taskRepository, IUserRepository _userRepository)
		{
			taskRepository = _taskRepository;
			userRepository = _userRepository;
		}

		public async Task Handle(MarkUserTaskAsDoneCommand markUserTaskAsDoneCommand, CancellationToken cancellationToken)
		{
			await taskRepository.MarkUserTaskAsDoneAsync(markUserTaskAsDoneCommand.UserTaskId);

		}
	}
}
