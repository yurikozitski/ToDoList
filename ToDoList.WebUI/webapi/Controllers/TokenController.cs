using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Infrastructure.DTOs;
using ToDoList.Infrastructure.Mediator.Commands;

namespace webapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IValidator<RefreshTokenCommand> tokenValidator;

        public TokenController(IMediator _mediator, IValidator<RefreshTokenCommand> _tokenValidator)
        {
            mediator = _mediator;
            tokenValidator = _tokenValidator;
        }

        [HttpPost("Refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TokenDto>> RefreshAsync(RefreshTokenCommand tokenCommand)
        {
            var validationResult = await tokenValidator.ValidateAsync(tokenCommand);

            if (!validationResult.IsValid)
            {
                return BadRequest("Invalid tokens");
            }

            return await mediator.Send(tokenCommand);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("Revoke")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RevokeAsync()
        {
            await mediator.Send(new RevokeTokenCommand() { Email = User?.Identity?.Name! });
            return NoContent();
        }
    }
}
