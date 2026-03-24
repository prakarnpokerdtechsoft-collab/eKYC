using Onboarding.Infrastructure.Persistence.Onboarding.Repositories;

namespace Onboarding.Core.Interfaces.Onboarding
{
    public interface IOnboardingUnitOfWork
    {
        IUserRepository Users { get; }
        Task<int> SaveChangesAsync();
    }
}
