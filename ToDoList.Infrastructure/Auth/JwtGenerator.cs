using System.Security.Claims;
using System.Text;
using ToDoList.Core.AuthInterfaces;
using ToDoList.Core.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace ToDoList.Infrastructure.Auth
{
    public class JwtGenerator : IJwtGenerator
	{
		private readonly SymmetricSecurityKey key;

		public JwtGenerator(IConfiguration config)
		{
			key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token"]!));
		}

		public string CreateToken(User user)
		{
			var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Email!) };

			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Issuer = "Issuer",
				Audience= "Audience",
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddHours(24),
				SigningCredentials = credentials
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
	}
}
