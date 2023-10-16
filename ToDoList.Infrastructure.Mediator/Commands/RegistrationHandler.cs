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
using ToDoList.Core.RepositoryInterfaces;

namespace ToDoList.Infrastructure.Mediator.Commands
{
	public class RegistrationHandler : IRequestHandler<RegistrationCommand, UserDTO>
	{
		private readonly IUserRepository userRepository;
		private readonly IJwtGenerator jwtGenerator;
		private readonly ApplicationContext context;

		public RegistrationHandler(ApplicationContext _context, IUserRepository _userRepository, IJwtGenerator _jwtGenerator)
		{
			context = _context;
			userRepository = _userRepository;
			jwtGenerator = _jwtGenerator;
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
				FirstName = request.FirstName,
				LastName = request.LastName,
				FullName = request.FirstName+" "+request.LastName,
				Email = request.Email,
				UserName="_"+request.Email
			};

			var result = await userRepository.CreateAsync(user, request.Password);

			if (result)
			{
				return new UserDTO
				{
					FullName = user.FullName,
					Token = jwtGenerator.CreateToken(user),
					ImagePath = null
				};
			}

			throw new Exception("Client creation failed");
		}
	}
}
