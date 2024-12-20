using MediatR;
using ToDoList.Core.Models;
using ToDoList.Core.RepositoryInterfaces;

namespace ToDoList.Infrastructure.Mediator.Queries
{
    public class GetTaskListsHandler : IRequestHandler<GetTaskListsQuery, List<TaskList>>
	{
		private readonly ITaskRepository taskRepository;

		public GetTaskListsHandler(ITaskRepository _taskRepository) 
		{
			taskRepository = _taskRepository;
		}

		public async Task<List<TaskList>> Handle(GetTaskListsQuery query, CancellationToken cancellationToken)
		{
			var taskLists = await taskRepository.GetTaskListsAsync(query.UserEmail!);
			return taskLists.ToList();
		}
	}
}
