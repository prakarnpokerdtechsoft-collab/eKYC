using Onboarding.Core.Interfaces.Services;
using Onboarding.Models;
using System.Collections.Concurrent;

namespace Onboarding.Infrastructure.Services.External
{
    public class OtpService : IOtpService
    {
        private static readonly ConcurrentDictionary<string, (string otp, DateTime expire, string refCode)> _store
           = new();

        private readonly Random _random = new();

        public ResponseDTO.GenerateOtp GenerateOtp(string reference)
        {
            var otp = _random.Next(100000, 999999).ToString(); // 6 digit
            var expire = DateTime.UtcNow.AddMinutes(5);
            var refCode = GenerateShortCode(); // เช่น A7K9P2
            do
            {
                refCode = GenerateShortCode();
            } while (_store.ContainsKey(refCode));

            _store[reference] = (otp, expire, refCode);
            var result = new ResponseDTO.GenerateOtp()
            {
                Otp = otp,
                Expire = expire,
                RefCode = refCode,
                reference = reference
            };

            return result;
        }

        private string GenerateShortCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();

            return new string(Enumerable.Range(0, 6)
                .Select(_ => chars[random.Next(chars.Length)]).ToArray());
        }

        public bool CheckOtp(RequestDTO.Otp request)
        {
            if (_store.TryGetValue(request.Reference!, out var data))
            {
                if(data.otp == request.OTP) return true;
            }
            return false;
        }
    }
}
