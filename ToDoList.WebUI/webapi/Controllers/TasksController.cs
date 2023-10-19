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

		[HttpGet("{action}/{taskListName}")]
		public async Task<IActionResult> AddTaskList([FromQuery][FromRoute]string? taskListName)
		{
			await mediator.Send(new AddTaskListCommand() { UserEmail = User.Identity?.Name, TaskListName = taskListName });
			return Ok();
		}
	}
}
