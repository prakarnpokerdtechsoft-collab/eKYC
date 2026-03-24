using Onboarding.Core.Interfaces.Helper;
using Onboarding.Core.Interfaces.Services;
using Onboarding.Helper;
using Onboarding.Infrastructure.Services.External;

namespace Onboarding.Extensions
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IMinioService, MinioService>();
            services.AddScoped<IDocumentPipelineService, DocumentPipelineService>();
            services.AddScoped<IEkycService, EkycService>();
            services.AddScoped<IClientSetting, ClientSetting>();
            return services;
        }
    }
}
