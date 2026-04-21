using BDA.Application.Authentication.Common;
using MediatR;
using ErrorOr;

namespace BDA.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;