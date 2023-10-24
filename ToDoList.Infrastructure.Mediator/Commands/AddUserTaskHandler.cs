using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Core.RepositoryInterfaces;
using ToDoList.Infrastructure.Exeptions;

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
			try
			{
				await taskRepository.AddUserTaskAsync(addUserTaskCommand.TaskListId, addUserTaskCommand.Text);
			}
			catch (NullReferenceException ex) 
			{
				throw new RestException(HttpStatusCode.BadRequest, ex.Message);
			}
			catch 
			{
				throw;
			}

		}
	}
}
