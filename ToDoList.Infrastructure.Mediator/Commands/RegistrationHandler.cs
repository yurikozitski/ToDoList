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
using ToDoList.Infrastructure.Exeptions;
using ToDoList.Core.RepositoryInterfaces;
using System.IO;
using ToDoList.Core.ServiceInterfaces;
using FluentValidation;
using FluentValidation.Results;

namespace ToDoList.Infrastructure.Mediator.Commands
{
	public class RegistrationHandler : IRequestHandler<RegistrationCommand, UserDTO>
	{
		private readonly IUserRepository userRepository;
		private readonly IJwtGenerator jwtGenerator;
		private readonly IImageValidator imageValidator;
		private readonly IValidator<RegistrationCommand> commandValidator;

		public RegistrationHandler(IUserRepository _userRepository,
			IJwtGenerator _jwtGenerator, 
			IImageValidator _imageValidator, 
			IValidator<RegistrationCommand> _commandValidator)
		{
			userRepository = _userRepository;
			jwtGenerator = _jwtGenerator;
			imageValidator = _imageValidator;
			commandValidator = _commandValidator;
		}

		public async Task<UserDTO> Handle(RegistrationCommand request, CancellationToken cancellationToken)
		{
			ValidationResult validationResult = await commandValidator.ValidateAsync(request);

			if (!validationResult.IsValid)
			{
				string? mes = string.Empty;
				foreach (var error in validationResult.Errors)
				{
					mes += $"{error.PropertyName} : {error.ErrorMessage}; ";
				}

				throw new Exception(mes);
			}

			if (await userRepository.GetByEmailAsync(request.Email!)!=null)
			{
				throw new RestException(HttpStatusCode.BadRequest, "Email already exist" );
			}

			string? relativeImagePath=null;
			byte[]? imageByteArryay=null;

			if (request.Image != null)
			{
				if (imageValidator.IsImageValid(request.Image))
				{
					var fileExtension = Path.GetExtension(request.Image.FileName);
					relativeImagePath = Path.Combine("UserUploads\\UserProfilePictures", request.Email + fileExtension);
					var fullImagePath = Path.Combine(Directory.GetCurrentDirectory(), relativeImagePath);

					using (var fileStream = new FileStream(fullImagePath, FileMode.Create))
					{
						await request.Image.CopyToAsync(fileStream);
						fileStream.Seek(0, SeekOrigin.Begin);

						using (var memoryStream = new MemoryStream())
						{
							await fileStream.CopyToAsync(memoryStream);
							imageByteArryay = memoryStream.ToArray();
						}
					}
				}
				else
				{
					throw new Exception("Profile picture should not be larger than 2Mb and have .jpg, .jpeg or .png extension");
				}
				
			}
			else
			{
				relativeImagePath = Path.Combine("UserUploads\\DefaultProfilePicture", "DefaultProfilePicture.jpg");
				var fullImagePath = Path.Combine(Directory.GetCurrentDirectory(), relativeImagePath);

				using (var fileStream = new FileStream(fullImagePath, FileMode.Open))
				{
					using (var memoryStream = new MemoryStream())
					{
						await fileStream.CopyToAsync(memoryStream);
						imageByteArryay = memoryStream.ToArray();
					}
				}
			}
			
			var user = new User
			{
				FirstName = request.FirstName,
				LastName = request.LastName,
				FullName = request.FirstName+" "+request.LastName,
				Email = request.Email,
				ImagePath =relativeImagePath,
				UserName="_"+request.Email
			};

			var result = await userRepository.CreateAsync(user, request.Password!);

			if (result)
			{
				return new UserDTO
				{
					FullName = user.FullName,
					Token = jwtGenerator.CreateToken(user),
					ImageData = Convert.ToBase64String(imageByteArryay)
				};
			}

			throw new Exception("User creation failed");
		}
	}
}
