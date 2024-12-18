using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Core.Models;

namespace ToDoList.Core.RepositoryInterfaces
{
	public interface ITaskRepository
	{
		public Task<IEnumerable<TaskList>> GetTaskListsAsync(string? userEmail);
		public Task AddTaskListAsync(string? taslListName, User? user);
		public Task AddUserTaskAsync(Guid taskListId, string? text);
		public Task MarkUserTaskAsDoneAsync(Guid userTaskId);
		public Task MarkUserTaskAsImportantAsync(Guid userTaskId);
		public Task AddUserTaskPlannedTimeAsync(Guid userTaskId,DateTime dateTime);
		public Task<IEnumerable<UserTask>> GetAllUserTasksAsync(string? userEmail);
		public Task<IEnumerable<UserTask>> GetUserTasksByTaskListAsync(Guid taskListId);
		public Task<IEnumerable<UserTask>> GetPlannedTasksAsync(string? userEmail);
		public Task<IEnumerable<UserTask>> GetImportantTasksAsync(string? userEmail);

	}
}
