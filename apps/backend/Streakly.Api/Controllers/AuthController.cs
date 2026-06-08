using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streakly.Api.Auth;
using Streakly.Application.Security;
using Streakly.Application.Users.Commands.ConfirmEmail;
using Streakly.Application.Users.Commands.Logout;
using Streakly.Application.Users.Commands.SignIn;
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

    [HttpPost("sign-in")]
    public async Task<ActionResult> SignIn(SignInRequest request)
    {
        var signInCommand = new SignInCommand(
            request.Email,
            request.Password);
        
        var jwt = await mediator.Send(signInCommand);
        
        return Ok(jwt);
    }

    [HttpPost("confirm-email")]
    public async Task<ActionResult> ConfirmEmail(ConfirmEmailRequest request)
    {
        await  mediator.Send(new ConfirmEmailCommand(request.Token));
        
        return NoContent();
    }

    [HttpGet("confirm-email")]
    public async Task<ActionResult> ConfirmEmailFromLink(string token)
    {
        await  mediator.Send(new ConfirmEmailCommand(token));
        
        return Content("Email confirmed. You can close this page.");
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        var command = new LogoutCommand(
            User.GetUserIdOrThrow(),
            User.GetTokenIdOrThrow(),
            User.GetExpirationUtcOrThrow());
        
        await mediator.Send(command);
        
        return NoContent();
    }
}
