using System.Security.Claims;
using System.Text;
using ToDoList.Core.AuthInterfaces;
using ToDoList.Core.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace ToDoList.Infrastructure.Auth
{
    public class TokenService : ITokenService
	{
		private readonly SymmetricSecurityKey key;

		public TokenService(IConfiguration config)
		{
			key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token"]!));
		}

		public string GenerateAccessToken(User user)
		{
			var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Email!) };

			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Issuer = "Issuer",
				Audience= "Audience",
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddHours(24),
				SigningCredentials = credentials
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateLifetime = false,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512Signature, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
    }
}
