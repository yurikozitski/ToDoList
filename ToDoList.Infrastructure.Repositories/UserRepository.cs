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
		private readonly SignInManager<User> signInManager;

		public UserRepository(
			UserManager<User> _userManager,
			SignInManager<User> _signInManager
			) 
		{
			userManager = _userManager;
			signInManager = _signInManager;
		}
		
		public async Task<bool> CreateAsync(User user, string password)
		{
			var result = await userManager.CreateAsync(user, password);
			return result.Succeeded;
		}

		public async Task<bool> SignInAsync(User user, string password)
		{
			var result = await signInManager.CheckPasswordSignInAsync(user, password, false);
			return result.Succeeded;
		}

		public Task SignOutAsync()
		{
			return signInManager.SignOutAsync();
		}
	}
}
