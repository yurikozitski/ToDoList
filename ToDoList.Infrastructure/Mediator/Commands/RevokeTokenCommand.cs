using MediatR;

namespace ToDoList.Infrastructure.Mediator.Commands
{
    public class RevokeTokenCommand : IRequest
    {
        public string Email { get; set; } = default!;
    }
}
