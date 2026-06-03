using MediatR;
using Microsoft.AspNetCore.Mvc;
using Streakly.Application.Users.Commands.SignUp;

namespace Streakly.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("sign-up")]
    public async Task<ActionResult> SignUp(SignUpRequest request)
    {
        var signUpCommand = new SignUpCommand(
            request.Email,
            request.UserName,
            request.Password,
            request.FullName);

        await mediator.Send(signUpCommand);

        return Created();
    }
}
