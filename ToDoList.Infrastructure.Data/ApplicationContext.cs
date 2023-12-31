﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ToDoList.Infrastructure.Data
{
	public class ApplicationContext : IdentityDbContext<User>
	{
		public DbSet<TaskList> TaskLists { get; set; }
		public DbSet<UserTask> UserTasks { get; set; }
		public ApplicationContext(DbContextOptions<ApplicationContext> options)
			: base(options)
		{
			//Database.EnsureDeleted();
			//Database.EnsureCreated();
		}
	}
}
