using BDA.Application.Common.Interfaces.Authentication;
using BDA.Application.Common.Interfaces.Persistence;
using BDA.Domain.Entities;

namespace BDA.Application.Services.Authentication;

public sealed class AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository) : IAuthenticationService
{
    public AuthenticationResult Login(string email, string password)
    {
        if (userRepository.GetUserByEmail(email) is not { } user)
        {
            throw new ArgumentException("User with email is not registered");
        }

        if (user.Password != password)
        {
            throw new ArgumentException("Passwords do not match");
        }
        
        var token = jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResult(user, token);
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        if (userRepository.GetUserByEmail(email) is not null)
        {
            throw new ArgumentException("User with email is already registered");
        }

        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password,
            Id = Guid.NewGuid(),
        };
        
        userRepository.Add(user);
        
        var token = jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}