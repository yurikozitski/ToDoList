﻿using ToDoList.Core.RepositoryInterfaces;
using ToDoList.Core.Models;
using ToDoList.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
	{
		private readonly ApplicationContext db;

		public TaskRepository(ApplicationContext _context) 
		{
			db = _context;
		}
		public async Task<IEnumerable<TaskList>> GetTaskListsAsync(string userEmail)
		{
			return await db.TaskLists.Where(tl => tl.User.Email == userEmail).ToListAsync();
		}

		public async Task AddTaskListAsync(string taskListName, User user)
		{
			TaskList taskList = new TaskList()
			{
				TaskListName = taskListName,
				User = user
			};

			await db.TaskLists.AddAsync(taskList);
			await db.SaveChangesAsync();
		}

		public async Task AddUserTaskAsync(Guid taskListId, string text)
		{
			TaskList? taskList = await db.TaskLists.FindAsync(taskListId);

            if (taskList != null)
            {
				UserTask userTask = new UserTask()
				{
					TaskList = taskList,
					Text = text,
				};

				await db.UserTasks.AddAsync(userTask);
				await db.SaveChangesAsync();                
            }
		}

		public async Task MarkUserTaskAsDoneAsync(Guid userTaskId)
		{
			UserTask? userTask= await db.UserTasks.FirstOrDefaultAsync(t => t.Id == userTaskId);

			if (userTask != null) 
			{
				userTask.IsDone = !userTask.IsDone;
				await db.SaveChangesAsync();
			}
		}

		public async Task MarkUserTaskAsImportantAsync(Guid userTaskId)
		{
			UserTask? userTask = await db.UserTasks.FirstOrDefaultAsync(t => t.Id == userTaskId);

			if (userTask != null)
			{
				userTask.IsImportant = !userTask.IsImportant;
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

		public async Task<IEnumerable<UserTask>> GetAllUserTasksAsync(string userEmail)
		{
			var userTasks = await db.UserTasks.Where(ut => ut.TaskList.User.Email == userEmail).ToListAsync();
			return userTasks;
		}

		public async Task<IEnumerable<UserTask>> GetUserTasksByTaskListAsync(Guid taskListId)
		{
			var userTasks = await db.UserTasks.Where(ut => ut.TaskList.Id == taskListId).ToListAsync();
			return userTasks;
		}

		public async Task<IEnumerable<UserTask>> GetPlannedTasksAsync(string userEmail)
		{
			DateTime defaultDate = new DateTime();
			var userTasks = await db.UserTasks.Where(ut => ut.TaskList.User.Email == userEmail && ut.PlannedTime > defaultDate).ToListAsync();
			return userTasks;
		}

		public async Task<IEnumerable<UserTask>> GetImportantTasksAsync(string userEmail)
		{
			var userTasks = await db.UserTasks.Where(ut => ut.IsImportant && ut.TaskList.User.Email == userEmail).ToListAsync();
			return userTasks;
		}
	}
}
