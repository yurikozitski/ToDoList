using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Core.Models;
using ToDoList.Infrastructure.Mediator.Commands;
using ToDoList.Infrastructure.Mediator.Queries;

namespace webapi.Controllers
{
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ApiController]
	[Route("[controller]/[action]")]
	public class TasksController : ControllerBase
	{
		private readonly IMediator mediator;

		public TasksController(IMediator _mediator)
		{
			mediator = _mediator;
		}

		[HttpGet]
		public async Task<ActionResult<List<TaskList>>> GetTaskLists()
		{
			return await mediator.Send(new GetTaskListsQuery() { UserEmail = User.Identity?.Name });
		}

		[HttpPost]
		public async Task<IActionResult> AddTaskList(AddTaskListCommand addTaskListCommand)
		{
			if (addTaskListCommand.TaskListName != null) 
			{
				addTaskListCommand.UserEmail = User.Identity?.Name;
				await mediator.Send(addTaskListCommand);
				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpPost]
		public async Task<IActionResult> AddUserTask(AddUserTaskCommand addUserTaskCommand)
		{
			await mediator.Send(addUserTaskCommand);
			return Ok();
		}

		[HttpPatch]
		public async Task<IActionResult> MarkUserTaskAsDone(MarkUserTaskAsDoneCommand markUserTaskAsDoneCommand)
		{
			await mediator.Send(markUserTaskAsDoneCommand);
			return Ok();
		}

		[HttpPatch]
		public async Task<IActionResult> MarkUserTaskAsImportant(MarkUserTaskAsImportantCommand markUserTaskAsImportantCommand)
		{
			await mediator.Send(markUserTaskAsImportantCommand);
			return Ok();
		}

		[HttpPatch]
		public async Task<IActionResult> AddUserTaskPlannedTime(AddUserTaskPlannedTimeCommand addUserTaskPlannedTimeCommand)
		{
			
			await mediator.Send(addUserTaskPlannedTimeCommand);
			return Ok();
		}

		[HttpGet]
		public async Task<ActionResult<List<UserTask>>> GetAllUserTasks()
		{

			return await mediator.Send(new GetAllUserTasksQuery() { UserEmail = User.Identity?.Name });
		}

		[HttpGet]
		public async Task<ActionResult<List<UserTask>>> GetUserTasksByTaskList([FromQuery]string taskListId)
		{
			return await mediator.Send(new GetUserTasksByTaskListQuery() { TaskListId = Guid.Parse(taskListId) });
		}

		[HttpGet]
		public async Task<ActionResult<List<UserTask>>> GetPlannedTasks()
		{

			return await mediator.Send(new GetPlannedTasksQuery() { UserEmail = User.Identity?.Name });
		}

		[HttpGet]
		public async Task<ActionResult<List<UserTask>>> GetImportantTasks()
		{
			return await mediator.Send(new GetImportantTasksQuery() { UserEmail = User.Identity?.Name });
		}
	}
}
