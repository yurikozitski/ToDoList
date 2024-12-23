using MediatR;
using System.Net;
using ToDoList.Core.Models;
using ToDoList.Core.AuthInterfaces;
using ToDoList.Infrastructure.DTOs;
using ToDoList.Infrastructure.Exeptions;
using ToDoList.Core.RepositoryInterfaces;
using ToDoList.Core.ServiceInterfaces;

namespace ToDoList.Infrastructure.Mediator.Commands
{
    public class RegistrationHandler : IRequestHandler<RegistrationCommand, UserDto>
	{
		private readonly IUserRepository userRepository;
		private readonly ITokenService tokenService;
		private readonly IImageValidator imageValidator;

		public RegistrationHandler(IUserRepository _userRepository,
			ITokenService _tokenService, 
			IImageValidator _imageValidator)
		{
			userRepository = _userRepository;
			tokenService = _tokenService;
			imageValidator = _imageValidator;
		}

		public async Task<UserDto> Handle(RegistrationCommand request, CancellationToken cancellationToken)
		{
			if (await userRepository.GetByEmailAsync(request.Email!) != null)
			{
				throw new RestException(HttpStatusCode.BadRequest, "Email already exist" );
			}

			string? relativeImagePath = null;
			byte[]? imageByteArryay = null;

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
                    throw new RestException(HttpStatusCode.BadRequest, "Profile picture should not be larger than 2Mb and have .jpg, .jpeg or .png extension");
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
				FullName = request.FirstName + " " + request.LastName,
				Email = request.Email,
				ImagePath = relativeImagePath,
				UserName = "_" + request.Email
			};

			var result = await userRepository.CreateAsync(user, request.Password!);

			if (!result)
			{
				throw new RestException(HttpStatusCode.InternalServerError, "User creation failed");				
			}

            string refreshToken = tokenService.GenerateRefreshToken();

            await userRepository.UpdateTokenAsync(user.Email!, refreshToken, DateTime.UtcNow.AddDays(14));

            return new UserDto
            {
                FullName = user.FullName,
                ImageData = Convert.ToBase64String(imageByteArryay),
                Token = tokenService.GenerateAccessToken(user),
                RefreshToken = refreshToken,
            };
        }
	}
}
