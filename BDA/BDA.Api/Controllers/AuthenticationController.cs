using BDA.Application.Authentication.Commands.Register;
using BDA.Application.Authentication.Common;
using BDA.Application.Authentication.Queries.Login;
using Microsoft.AspNetCore.Mvc;

using BDA.Contracts.Authentication;
using BDA.Domain.Common.Errors;
using MediatR;

namespace BDA.Api.Controllers;

[Route("auth")]
public class AuthenticationController(ISender mediator) : ApiController
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);

        var result = await mediator.Send(command);

        return result.Match(
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
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = new LoginQuery(request.Email, request.Password);

        var authResult = await mediator.Send(query);
        if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);

        return authResult.Match(
            value => Ok(MapAuthResult(value)),
            Problem);
    }
}