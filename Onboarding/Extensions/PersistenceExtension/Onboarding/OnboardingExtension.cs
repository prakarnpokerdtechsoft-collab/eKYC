using Microsoft.EntityFrameworkCore;
using Onboarding.Core.Interfaces.Onboarding;
using Onboarding.Infrastructure.Persistence.Onboarding;
using Onboarding.Infrastructure.Persistence.Onboarding.Entities;
using Onboarding.Infrastructure.Persistence.Onboarding.Repositories;

namespace Onboarding.Extensions.Persistence.Onboarding
{
    public static class OnboardingExtension
    {
        public static IServiceCollection AddOnboarding(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("OnboardingContext")
            ?? throw new InvalidOperationException("Connection string not found.");

            services.AddDbContext<OnboardingContext>(options =>
                options.UseSqlServer(connectionString, sql =>
                    sql.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null)));

            services.AddDefaultIdentity<OnboardingUser>(options =>
                options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<OnboardingContext>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOnboardingUnitOfWork, OnboardingUnitOfWork>();
            return services;
        }
    }
}
