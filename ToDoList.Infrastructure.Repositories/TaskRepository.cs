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

		public async Task AddTaskListAsync(string? taskListName, User? user)
		{
			TaskList taskList=new TaskList()
			{
				TaskListName = taskListName,
				User = user!
			};

			await db.TaskLists.AddAsync(taskList);
			await db.SaveChangesAsync();
		}

		public async Task AddUserTaskAsync(Guid taskListId, string? text)
		{

			TaskList? taskList=await db.TaskLists.FindAsync(taskListId);

			UserTask userTask = new UserTask()
			{
				TaskList=taskList?? throw new NullReferenceException("Null task id"),
				Text = text
			};

			await db.UserTasks.AddAsync(userTask);
			await db.SaveChangesAsync();
		}

		public async Task MarkUserTaskAsDoneAsync(Guid userTaskId)
		{
			UserTask? userTask= await db.UserTasks.FirstOrDefaultAsync(t=>t.Id==userTaskId);

			if (userTask!=null) 
			{
				userTask.IsDone = true;
				await db.SaveChangesAsync();
			}
		}

		public async Task MarkUserTaskAsImportantAsync(Guid userTaskId)
		{
			UserTask? userTask = await db.UserTasks.FirstOrDefaultAsync(t => t.Id == userTaskId);

			if (userTask != null)
			{
				userTask.IsImportant = true;
				await db.SaveChangesAsync();
			}
		}

		public async Task AddUserTaskPlannedTimeAsync(Guid userTaskId, DateTime dateTime)
		{
			UserTask? userTask = await db.UserTasks.FirstOrDefaultAsync(t => t.Id == userTaskId);

			if (userTask != null)
			{
				userTask.PlannedTime = dateTime;
				await db.SaveChangesAsync();
			}
		}

		public async Task<IEnumerable<UserTask>> GetAllUserTasksAsync(string? userEmail)
		{
			var userTasks = await db.UserTasks.Where(ut => ut.TaskList.User.Email == userEmail).ToListAsync();
			return userTasks;
		}

		public async Task<IEnumerable<UserTask>> GetUserTasksByTaskListAsync(Guid taskListId)
		{
			var userTasks = await db.UserTasks.Where(ut => ut.TaskList.Id==taskListId).ToListAsync();
			return userTasks;
		}
	}
}
