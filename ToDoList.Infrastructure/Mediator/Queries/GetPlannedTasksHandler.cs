using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Core.Models;
using ToDoList.Core.RepositoryInterfaces;

namespace ToDoList.Infrastructure.Mediator.Queries
{
	public class GetPlannedTasksHandler : IRequestHandler<GetPlannedTasksQuery, List<UserTask>>
	{
		private readonly ITaskRepository taskRepository;

		public GetPlannedTasksHandler(ITaskRepository _taskRepository)
		{
			taskRepository = _taskRepository;
		}

		public async Task<List<UserTask>> Handle(GetPlannedTasksQuery query, CancellationToken cancellationToken)
		{
			var taskLists = await taskRepository.GetPlannedTasksAsync(query.UserEmail);
			return taskLists.ToList();
		}
	}
}
