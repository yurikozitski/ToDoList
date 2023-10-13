using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Core.AuthInterfaces;
using ToDoList.Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace ToDoList.Infrastructure.Auth
{
	public class JwtGenerator : IJwtGenerator
	{
		private readonly SymmetricSecurityKey _key;

		public JwtGenerator(IConfiguration config)
		{
			_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token"]));
		}

		public string CreateToken(User user)
		{
			var claims = new List<Claim> { new Claim(JwtRegisteredClaimNames.NameId, user.Email) };

			var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(2),
				SigningCredentials = credentials
			};
			var tokenHandler = new JwtSecurityTokenHandler();

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
	}
}
