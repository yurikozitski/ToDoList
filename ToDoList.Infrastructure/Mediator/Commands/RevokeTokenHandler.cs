using MediatR;
using ToDoList.Core.RepositoryInterfaces;
using ToDoList.Infrastructure.Exeptions;

namespace ToDoList.Infrastructure.Mediator.Commands
{
    public class RevokeTokenHandler : IRequestHandler<RevokeTokenCommand>
    {
        private readonly IUserRepository userRepository;

        public RevokeTokenHandler(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }

        public async Task Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByEmailAsync(request.Email);

            if (user == null)
            {
                throw new TokenException("Can't revoke user token");
            }

            await userRepository.UpdateTokenAsync(request.Email, null, null);
        }
    }
}
