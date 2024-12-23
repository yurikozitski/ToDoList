using System.Security.Claims;
using ToDoList.Core.Models;

namespace ToDoList.Core.AuthInterfaces
{
    public interface ITokenService
	{
		string GenerateAccessToken(User user);

        string GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
