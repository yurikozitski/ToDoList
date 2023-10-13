using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ToDoList.Core.Models
{
	public class User:IdentityUser
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? FullName { get; set; }
		public string? ImagePath { get; set; }
	}
}
