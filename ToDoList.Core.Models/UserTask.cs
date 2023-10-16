using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Core.Models
{
	public class UserTask
	{
		public Guid Id { get; set; }
		public string? Text { get; set; }
		public DateTime PlannedTime { get; set; }
		public bool IsDone { get; set; }
		public bool IsImportant { get; set; }
		public TaskList TaskList { get; set; } = new TaskList();
	}
}
