using Microsoft.AspNetCore.Identity;

namespace ToDoList.Core.Models
{
    public class User : IdentityUser
	{
		public string FirstName { get; set; } = default!;

		public string LastName { get; set; } = default!;

		public string FullName { get; set; } = default!;

		public string? ImagePath { get; set; }

		public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }

        public List<TaskList> TaskLists { get; set; } = new List<TaskList>();
	}
}
