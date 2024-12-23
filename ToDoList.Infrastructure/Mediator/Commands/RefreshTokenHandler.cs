using MediatR;
using ToDoList.Core.AuthInterfaces;
using ToDoList.Core.RepositoryInterfaces;
using ToDoList.Infrastructure.DTOs;
using ToDoList.Infrastructure.Exeptions;

namespace ToDoList.Infrastructure.Mediator.Commands
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, TokenDto>
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenService tokenService;

        public RefreshTokenHandler(IUserRepository _userRepository,
            ITokenService _tokenService)
        {
            userRepository = _userRepository;
            tokenService = _tokenService;
        }

        public async Task<TokenDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            string accessToken = request.Token;
            string refreshToken = request.RefreshToken;

            var principal = tokenService.GetPrincipalFromExpiredToken(accessToken);
            var userEmail = principal.Identity?.Name;

            var user = await userRepository.GetByEmailAsync(userEmail!);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new TokenException("Invalid user token or token expired");
            }

            string? newAccessToken = tokenService.GenerateAccessToken(user);
            string? newRefreshToken = tokenService.GenerateRefreshToken();

            await userRepository.UpdateTokenAsync(userEmail!, newRefreshToken, user.RefreshTokenExpiryTime);

            return new TokenDto()
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken,
            };
        }
    }
}
