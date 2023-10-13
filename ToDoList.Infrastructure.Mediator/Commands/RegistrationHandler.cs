using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Core.Models;
using ToDoList.Core.AuthInterfaces;
using ToDoList.Infrastructure.DTOs;
using ToDoList.Infrastructure.Data;

namespace ToDoList.Infrastructure.Mediator.Commands
{
	public class RegistrationHandler : IRequestHandler<RegistrationCommand, UserDTO>
	{
		private readonly UserManager<User> _userManager;
		private readonly IJwtGenerator _jwtGenerator;
		private readonly ApplicationContext _context;

		public RegistrationHandler(ApplicationContext context, UserManager<User> userManager, IJwtGenerator jwtGenerator)
		{
			_context = context;
			_userManager = userManager;
			_jwtGenerator = jwtGenerator;
		}

		public async Task<UserDTO> Handle(RegistrationCommand request, CancellationToken cancellationToken)
		{
			//if (await _context.Users.Where(x => x.Email == request.Email).AnyAsync())
			//{
			//	throw new RestException(HttpStatusCode.BadRequest, new { Email = "Email already exist" });
			//}

			//if (await _context.Users.Where(x => x.UserName == request.UserName).AnyAsync())
			//{
			//	throw new RestException(HttpStatusCode.BadRequest, new { UserName = "UserName already exist" });
			//}

			var user = new User
			{
				DisplayName = request.DisplayName,
				Email = request.Email,
				UserName = request.UserName
			};

			var result = await _userManager.CreateAsync(user, request.Password);

			if (result.Succeeded)
			{
				return new User
				{
					DisplayName = user.DisplayName,
					Token = _jwtGenerator.CreateToken(user),
					UserName = user.UserName,
					Image = null
				};
			}

			throw new Exception("Client creation failed");
		}
	}
}
