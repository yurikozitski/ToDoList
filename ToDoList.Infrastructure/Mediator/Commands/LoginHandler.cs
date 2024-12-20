using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;
using ToDoList.Core.AuthInterfaces;
using ToDoList.Core.Models;
using ToDoList.Core.RepositoryInterfaces;
using ToDoList.Infrastructure.Exeptions;
using ToDoList.Infrastructure.DTOs;

namespace ToDoList.Infrastructure.Mediator.Commands
{
    public class LoginHandler : IRequestHandler<LoginCommand, UserDto>
	{
		private readonly IJwtGenerator jwtGenerator;
		private readonly IUserRepository userRepository;
		private readonly SignInManager<User> signInManager;
		
		public LoginHandler(IUserRepository _userRepository, 
			SignInManager<User> _signInManager,
			IJwtGenerator _jwtGenerator)
		{
			userRepository = _userRepository;
			signInManager = _signInManager;
			jwtGenerator = _jwtGenerator;
		}

		public async Task<UserDto> Handle(LoginCommand request, CancellationToken cancellationToken)
		{			
			var user = await userRepository.GetByEmailAsync(request.Email);

			if (user == null)
			{
				throw new RestException(HttpStatusCode.BadRequest, "User doesn't exist");
			}
 
			var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
			
			byte[]? imageByteArryay = null;

			if (result.Succeeded)
			{
				var relativeImagePath = user.ImagePath ?? Path.Combine("UserUploads\\DefaultProfilePicture", "DefaultProfilePicture.jpg");
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
			else
			{
                throw new RestException(HttpStatusCode.BadRequest, "Login failed, enter password again");
			}

            return new UserDto
            {
                FullName = user.FullName,
                Token = jwtGenerator.CreateToken(user),
                ImageData = Convert.ToBase64String(imageByteArryay)
            };
        }
	}
}
