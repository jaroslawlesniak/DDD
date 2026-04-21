using BDA.Domain.Entities;

namespace BDA.Application.Authentication.Common;

public record AuthenticationResult(User User, string Token);