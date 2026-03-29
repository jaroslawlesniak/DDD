using BDA.Application.Common.Interfaces.Services;

namespace BDA.Infrastructure.Services;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}