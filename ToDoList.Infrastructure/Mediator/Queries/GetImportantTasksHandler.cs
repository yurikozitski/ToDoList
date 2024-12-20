using MediatR;
using ToDoList.Core.Models;
using ToDoList.Core.RepositoryInterfaces;

namespace ToDoList.Infrastructure.Mediator.Queries
{
    public class GetImportantTasksHandler : IRequestHandler<GetImportantTasksQuery, List<UserTask>>
	{
		private readonly ITaskRepository taskRepository;

		public GetImportantTasksHandler(ITaskRepository _taskRepository)
		{
			taskRepository = _taskRepository;
		}

		public async Task<List<UserTask>> Handle(GetImportantTasksQuery query, CancellationToken cancellationToken)
		{
			var taskLists = await taskRepository.GetImportantTasksAsync(query.UserEmail!);
			return taskLists.ToList();
		}
	}
}
