using MediatR;
using ToDoList.Infrastructure.DTOs;

namespace ToDoList.Infrastructure.Mediator.Commands
{
    public class RefreshTokenCommand : IRequest<TokenDto>
    {
        public string Token { get; set; } = default!;

        public string RefreshToken { get; set; } = default!;
    }
}
