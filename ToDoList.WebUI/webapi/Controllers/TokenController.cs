using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Infrastructure.DTOs;

namespace webapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IMediator mediator;

        public TokenController(IMediator _mediator)
        {
            mediator = _mediator;
        }

        [HttpPost("Refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TokenDto>> RefreshAsync(RefreshTokenCommand refreshTokenCommand)
        {
            return await mediator.Send(refreshTokenCommand);
        }
    }
}
