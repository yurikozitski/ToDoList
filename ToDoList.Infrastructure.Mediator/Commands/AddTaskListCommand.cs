﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Infrastructure.Mediator.Commands
{
	public class AddTaskListCommand: IRequest
	{
		public string? TaskListName { get; set; }

		public string? UserEmail { get; set; }
	}
}
