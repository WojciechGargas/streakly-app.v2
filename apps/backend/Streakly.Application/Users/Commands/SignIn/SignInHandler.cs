using System.Security.Authentication;
using MediatR;
using Streakly.Application.DTO;
using Streakly.Application.Exceptions;
using Streakly.Application.Security;
using Streakly.Core.Abstractions;
using Streakly.Core.Repositories;

namespace Streakly.Application.Users.Commands.SignIn;

public class SignInHandler(
    IUserRepository userRepository,
    IAuthenticator authenticator,
    IPasswordManager passwordManager,
    ITokenStorage tokenStorage)
    : IRequestHandler<SignInCommand, JwtDto>
{
    public async Task<JwtDto> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByEmailAsync(request.Email) ??
                   throw new InvalidCredentialsException();
        
        if(!passwordManager.Validate(request.Password, user.Password))
            throw new InvalidCredentialsException();
        
        var jwt = authenticator.CreateToken(user.UserId,  user.Role.ToString());
        
        tokenStorage.Set(jwt);
        
        return jwt;
    }
}