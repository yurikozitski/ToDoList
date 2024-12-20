namespace ToDoList.Core.Models
{
    public class TaskList
	{
		public Guid Id { get; set; }

		public string TaskListName { get; set; } = default!;

		public User User { get; set; } = default!;

		public List<UserTask> Tasks { get; set; } = new List<UserTask>();
	}
}
