using BDA.Application.Authentication.Commands.Register;
using BDA.Application.Authentication.Queries.Login;
using Microsoft.AspNetCore.Mvc;

using BDA.Contracts.Authentication;
using BDA.Domain.Common.Errors;
using MapsterMapper;
using MediatR;

namespace BDA.Api.Controllers;

[Route("auth")]
public class AuthenticationController(ISender mediator, IMapper mapper) : ApiController
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command =  mapper.Map<RegisterCommand>(request);

        var result = await mediator.Send(command);

        return result.Match(
            value => Ok(mapper.Map<AuthenticationResponse>(value)),
            Problem);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = mapper.Map<LoginQuery>(request);

        var authResult = await mediator.Send(query);
        if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);

        return authResult.Match(
            value => Ok(mapper.Map<AuthenticationResponse>(value)),
            Problem);
    }
}