using Onboarding.Core.Interfaces.Services;
using Onboarding.Infrastructure.Services.External;

namespace Onboarding.Extensions;

public static class ApplicationExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IOtpService, OtpService>();
        return services;
    }
}
