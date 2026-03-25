using Onboarding.Models;

namespace Onboarding.Core.Interfaces.Services
{
    public interface IEkycService
    {
        public Task<string> CreateCase(RequestDTO.CreateCase model);
        public Task<string> GetVerification(RequestDTO.Verifications model);
    }
}
