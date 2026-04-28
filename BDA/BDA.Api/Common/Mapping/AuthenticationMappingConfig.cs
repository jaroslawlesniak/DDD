using BDA.Application.Authentication.Commands.Register;
using BDA.Application.Authentication.Common;
using BDA.Application.Authentication.Queries.Login;
using BDA.Contracts.Authentication;
using Mapster;

namespace BDA.Api.Common.Mapping;

public sealed class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();
        config.NewConfig<LoginRequest, LoginQuery>();
        
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Token, src => src.Token)
            .Map(desc => desc, src => src.User);
    }
}