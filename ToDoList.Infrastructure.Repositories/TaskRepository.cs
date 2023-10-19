using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Core.RepositoryInterfaces;
using ToDoList.Core.Models;
using ToDoList.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Infrastructure.Repositories
{
	public class TaskRepository:ITaskRepository
	{
		private readonly ApplicationContext db;

		public TaskRepository(ApplicationContext _context) 
		{
			db = _context;
		}
		public async Task<IEnumerable<TaskList>> GetTaskListsAsync(string? userEmail)
		{
			return await db.TaskLists.Where(tl => tl.User.Email == userEmail).ToListAsync();
		}

		public async Task AddTaskListAsync(string? taskListName, User user)
		{
			TaskList taskList=new TaskList()
			{
				TaskListName = taskListName,
				User = user
			};

			await db.TaskLists.AddAsync(taskList);
			await db.SaveChangesAsync();
		}
	}
}
