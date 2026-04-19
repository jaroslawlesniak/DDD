using BDA.Application.Services.Authentication;
using Microsoft.AspNetCore.Mvc;

using BDA.Contracts.Authentication;
using BDA.Domain.Common.Errors;

namespace BDA.Api.Controllers;

[Route("auth")]
public class AuthenticationController(IAuthenticationService _authenticationService) : ApiController
{
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var authResult = _authenticationService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        return authResult.Match(
            value => Ok(MapAuthResult(value)),
            Problem);
    }

    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = _authenticationService.Login(
            request.Email,
            request.Password);

        if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
        {
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);
        }

        return authResult.Match(
            value => Ok(MapAuthResult(value)),
            Problem);
    }
}