namespace BDA.Application.Common.Interfaces.Authentication;

public record class JwtSettings
{
    public const string SectionName = "JwtSettings";

    public string Secret { get; init; }
    
    public string Issuer { get; init; }
    
    public string Audience { get; init; }

    public int ExpiryInMinutes { get; init; }
}