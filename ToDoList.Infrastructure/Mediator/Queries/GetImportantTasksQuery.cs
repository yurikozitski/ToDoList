using MediatR;
using ToDoList.Core.Models;

namespace ToDoList.Infrastructure.Mediator.Queries
{
    public class GetImportantTasksQuery : IRequest<List<UserTask>>
	{
		public string UserEmail { get; set; } = default!;
	}
}
