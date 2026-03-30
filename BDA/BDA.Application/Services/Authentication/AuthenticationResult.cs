using BDA.Domain.Entities;

namespace BDA.Application.Services.Authentication;

public record AuthenticationResult(
    User User,
    string Token);