using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Core.Models
{
	public class TaskList
	{
		public Guid Id { get; set; }
		public User User { get; set; }
		public List<UserTask> Tasks { get; set; } = new List<UserTask>();
	}
}
