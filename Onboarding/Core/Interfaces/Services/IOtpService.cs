using Onboarding.Models;

namespace Onboarding.Core.Interfaces.Services
{
    public interface IOtpService
    {
        public ResponseDTO.GenerateOtp GenerateOtp(string reference);
        public bool CheckOtp(RequestDTO.Otp request);
    }
}
