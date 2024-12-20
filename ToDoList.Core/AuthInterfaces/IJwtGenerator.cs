using ToDoList.Core.Models;

namespace ToDoList.Core.AuthInterfaces
{
    public interface IJwtGenerator
	{
		string CreateToken(User user);
	}
}
