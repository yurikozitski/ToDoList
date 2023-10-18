using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Core.Models;

namespace ToDoList.Core.RepositoryInterfaces
{
	public interface IUserRepository
	{
		Task<bool> CreateAsync(User user, string password);
		//Task<bool> SignInAsync(User user, string password);
		//Task SignOutAsync();
		//Task<bool> ChangePasswordAsync(User user, string oldPassword, string newPassword);
		//Task<bool> UpDateAsync(User user);
		//Task<User> GetByIdAsync(string userId);
		Task<User> GetByEmailAsync(string email);
		//Task<User> GetByUniqueNameAsync(string uniqueName);
	}
}
