using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Core.AuthInterfaces;
using ToDoList.Core.Models;
using ToDoList.Core.RepositoryInterfaces;
using ToDoList.Infrastructure.Data;
using ToDoList.Infrastructure.Exeptions;
using ToDoList.Infrastructure.DTOs;
using FluentValidation;
using FluentValidation.Results;

namespace ToDoList.Infrastructure.Mediator.Commands
{
	public class LoginHandler : IRequestHandler<LoginCommand, UserDTO>
	{
		private readonly IJwtGenerator jwtGenerator;
		private readonly IUserRepository userRepository;
		private readonly SignInManager<User> signInManager;
		private readonly IValidator<LoginCommand> commandValidator;

		public LoginHandler(IUserRepository _userRepository, 
			SignInManager<User> _signInManager,
			IJwtGenerator _jwtGenerator,
			IValidator<LoginCommand> _commandValidator)
		{
			userRepository = _userRepository;
			signInManager = _signInManager;
			jwtGenerator = _jwtGenerator;
			commandValidator = _commandValidator;
		}

		public async Task<UserDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
		{

			ValidationResult validationResult = await commandValidator.ValidateAsync(request);

			if (!validationResult.IsValid)
			{
				string? mes = "";
				foreach (var error in validationResult.Errors)
				{
					mes += $"{error.PropertyName} : {error.ErrorMessage}; ";
				}

				throw new Exception(mes);
			}

			var user= await userRepository.GetByEmailAsync(request.Email);

			if (user == null)
			{
				throw new RestException(HttpStatusCode.BadRequest, "User doesn't exist");
			}
 
			var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
			
			if (result.Succeeded)
			{
				byte[]? imageByteArryay = null;

				var relativeImagePath = user.ImagePath?? Path.Combine("UserUploads\\DefaultProfilePicture", "DefaultProfilePicture.jpg");
				var fullImagePath = Path.Combine(Directory.GetCurrentDirectory(), relativeImagePath);

				using (var fileStream = new FileStream(fullImagePath, FileMode.Open))
				{
					using (var memoryStream = new MemoryStream())
					{
						await fileStream.CopyToAsync(memoryStream);
						imageByteArryay = memoryStream.ToArray();
					}
				}

				return new UserDTO
				{
					FullName = user.FullName,
					Token = jwtGenerator.CreateToken(user),
					ImageData = Convert.ToBase64String(imageByteArryay)
				};
			}

			throw new Exception("Login failed");
		}
	}
}
