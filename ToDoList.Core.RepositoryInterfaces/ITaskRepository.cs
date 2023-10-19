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
		public Task AddTaskListAsync(string? taslListName, User user);
	}
}
