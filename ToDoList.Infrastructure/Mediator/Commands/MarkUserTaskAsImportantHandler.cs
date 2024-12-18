using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Core.RepositoryInterfaces;

namespace ToDoList.Infrastructure.Mediator.Commands
{
	public class MarkUserTaskAsImportantHandler : IRequestHandler<MarkUserTaskAsImportantCommand>
	{
		private readonly ITaskRepository taskRepository;
		private readonly IUserRepository userRepository;

		public MarkUserTaskAsImportantHandler(ITaskRepository _taskRepository, IUserRepository _userRepository)
		{
			taskRepository = _taskRepository;
			userRepository = _userRepository;
		}

		public async Task Handle(MarkUserTaskAsImportantCommand markUserTaskAsImportantCommand, CancellationToken cancellationToken)
		{
			await taskRepository.MarkUserTaskAsImportantAsync(markUserTaskAsImportantCommand.UserTaskId);

		}
	}
}
