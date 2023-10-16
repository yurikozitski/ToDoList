using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Core.RepositoryInterfaces;
using ToDoList.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace ToDoList.Infrastructure.Repositories
{
	public class UserRepository: IUserRepository
	{
		private readonly UserManager<User> userManager;
		
		public UserRepository(
			UserManager<User> _userManager
			) 
		{
			userManager = _userManager;
		}
		
		public async Task<bool> CreateAsync(User user, string password)
		{
			var result = await userManager.CreateAsync(user, password);
			return result.Succeeded;
		}

	}
}
