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
	[Route("[controller]")]
	[ApiController]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class TasksController : ControllerBase
	{
		private readonly IMediator mediator;

		public TasksController(IMediator _mediator)
		{
			mediator = _mediator;
		}

		[HttpGet("{action}")]
		public async Task<ActionResult<List<TaskList>>> GetTaskLists()
		{
			return await mediator.Send(new GetTaskListsQuery() { UserEmail = User.Identity?.Name });
		}

		[HttpPost("{action}")]
		public async Task<IActionResult> AddTaskList(string? taskListName)
		{
			await mediator.Send(new AddTaskListCommand() { UserEmail = User.Identity?.Name, TaskListName = taskListName });
			return Ok();
		}

		[HttpPost("{action}")]
		public async Task<IActionResult> AddUserTask(AddUserTaskCommand addUserTaskCommand)
		{
			await mediator.Send(addUserTaskCommand);
			return Ok();
		}

		[HttpPatch("{action}")]
		public async Task<IActionResult> MarkUserTaskAsDone(string userTaskId)
		{
			await mediator.Send(new MarkUserTaskAsDoneCommand() {UserTaskId=Guid.Parse(userTaskId) });
			return Ok();
		}

		[HttpPatch("{action}")]
		public async Task<IActionResult> MarkUserTaskAsImportant(string userTaskId)
		{
			await mediator.Send(new MarkUserTaskAsImportantCommand() { UserTaskId = Guid.Parse(userTaskId) });
			return Ok();
		}

		[HttpPatch("{action}")]
		public async Task<IActionResult> AddUserTaskPlannedTime(AddUserTaskPlannedTimeCommand addUserTaskPlannedTimeCommand)
		{
			await mediator.Send(addUserTaskPlannedTimeCommand);
			return Ok();
		}

		[HttpGet("{action}")]
		public async Task<ActionResult<List<UserTask>>> GetAllUserTasks()
		{
			return await mediator.Send(new GetAllUserTasksQuery() {UserEmail=User.Identity?.Name });
		}

		[HttpGet("{action}")]
		public async Task<ActionResult<List<UserTask>>> GetUserTasksByTaskList([FromQuery]string taskListId)
		{
			try
			{
				return await mediator.Send(new GetUserTasksByTaskListQuery() { TaskListId = Guid.Parse(taskListId) });
			}
			catch (Exception ex) 
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
