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
			try
			{
				return await mediator.Send(new GetTaskListsQuery() { UserEmail = User.Identity?.Name });
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
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
			try
			{
				await mediator.Send(addUserTaskCommand);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPatch]
		public async Task<IActionResult> MarkUserTaskAsDone(MarkUserTaskAsDoneCommand markUserTaskAsDoneCommand)
		{
			try
			{
				await mediator.Send(markUserTaskAsDoneCommand);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPatch]
		public async Task<IActionResult> MarkUserTaskAsImportant(MarkUserTaskAsImportantCommand markUserTaskAsImportantCommand)
		{
			try
			{
				await mediator.Send(markUserTaskAsImportantCommand);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPatch]
		public async Task<IActionResult> AddUserTaskPlannedTime(AddUserTaskPlannedTimeCommand addUserTaskPlannedTimeCommand)
		{
			try
			{
				await mediator.Send(addUserTaskPlannedTimeCommand);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
		public async Task<ActionResult<List<UserTask>>> GetAllUserTasks()
		{
			try
			{
				return await mediator.Send(new GetAllUserTasksQuery() { UserEmail = User.Identity?.Name });
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
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

		[HttpGet]
		public async Task<ActionResult<List<UserTask>>> GetPlannedTasks()
		{
			try
			{
				return await mediator.Send(new GetPlannedTasksQuery() { UserEmail = User.Identity?.Name });
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
		public async Task<ActionResult<List<UserTask>>> GetImportantTasks()
		{
			try
			{
				return await mediator.Send(new GetImportantTasksQuery() { UserEmail = User.Identity?.Name });
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
