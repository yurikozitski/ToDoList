 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Core.Models;
using ToDoList.Core.RepositoryInterfaces;
using MediatR;

namespace ToDoList.Infrastructure.Mediator.Queries
{
	public class  GetAllUserTasksHandler : IRequestHandler< GetAllUserTasksQuery, List<UserTask>>
	{
		private readonly ITaskRepository taskRepository;

		public  GetAllUserTasksHandler(ITaskRepository _taskRepository)
		{
			taskRepository = _taskRepository;
		}

		public async Task<List<UserTask>> Handle(GetAllUserTasksQuery query, CancellationToken cancellationToken)
		{
			var taskLists = await taskRepository.GetAllUserTasksAsync(query.UserEmail);
			return taskLists.ToList();
		}
	}
}
