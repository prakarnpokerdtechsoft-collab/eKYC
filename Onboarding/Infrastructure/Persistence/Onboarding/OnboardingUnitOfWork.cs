using Onboarding.Core.Interfaces.Onboarding;
using Onboarding.Infrastructure.Persistence.Onboarding.Repositories;

namespace Onboarding.Infrastructure.Persistence.Onboarding
{
    public class OnboardingUnitOfWork : IOnboardingUnitOfWork
    {
        private readonly OnboardingContext _context;

        public IUserRepository Users { get; }

        public OnboardingUnitOfWork(OnboardingContext context, IUserRepository users)
        {
            _context = context;
            Users = users;
        }

        public Task<int> SaveChangesAsync()
            => _context.SaveChangesAsync();
    }
}
