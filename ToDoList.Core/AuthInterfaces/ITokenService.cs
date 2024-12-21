using ToDoList.Core.Models;

namespace ToDoList.Core.AuthInterfaces
{
    public interface ITokenService
	{
		string GenerateAccessToken(User user);
	}
}
