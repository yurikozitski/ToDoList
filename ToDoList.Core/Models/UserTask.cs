namespace ToDoList.Core.Models
{
    public class UserTask
	{
		public Guid Id { get; set; }

		public string Text { get; set; } = default!;

		public DateTime PlannedTime { get; set; }

		public bool IsDone { get; set; }

		public bool IsImportant { get; set; }

		public Guid TaskListId { get; set; }

		public TaskList TaskList { get; set; } = default!;
	}
}
