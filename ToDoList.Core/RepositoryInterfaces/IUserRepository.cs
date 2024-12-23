using ToDoList.Core.Models;

namespace ToDoList.Core.RepositoryInterfaces
{
    public interface IUserRepository
	{
		Task<bool> CreateAsync(User user, string password);

		Task<User?> GetByEmailAsync(string email);

		Task UpdateTokenAsync(string email ,string? token, DateTime? expiryDate);
	}
}
