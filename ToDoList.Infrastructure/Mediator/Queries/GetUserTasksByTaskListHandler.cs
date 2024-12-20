using MediatR;
using ToDoList.Core.Models;
using ToDoList.Core.RepositoryInterfaces;

namespace ToDoList.Infrastructure.Mediator.Queries
{
    public class GetUserTasksByTaskListHandler : IRequestHandler<GetUserTasksByTaskListQuery, List<UserTask>>
	{
		private readonly ITaskRepository taskRepository;

		public GetUserTasksByTaskListHandler(ITaskRepository _taskRepository)
		{
			taskRepository = _taskRepository;
		}

		public async Task<List<UserTask>> Handle(GetUserTasksByTaskListQuery query, CancellationToken cancellationToken)
		{
			var taskLists = await taskRepository.GetUserTasksByTaskListAsync(query.TaskListId);
			return taskLists.ToList();
		}
	}
}
