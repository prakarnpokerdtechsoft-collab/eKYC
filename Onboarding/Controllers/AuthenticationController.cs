using Microsoft.AspNetCore.Mvc;
using Onboarding.Core.Interfaces.Services;
using Onboarding.Models;

namespace Onboarding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IOtpService _otpService;
        public AuthenticationController(IOtpService otpService) 
        {
            _otpService = otpService;
        }

        [HttpPost]
        [Route("SignUp")]
        public IActionResult SignUp([FromBody] RequestDTO.SignUp request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var reference = $"OTP-{Guid.NewGuid():N}";
            var getotp = _otpService.GenerateOtp(reference);
            return Ok(getotp);
        }

        //[HttpPost]
        //[Route("ConfirmSignUp")]
        //public IActionResult


        [HttpPost]
        [Route("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] RequestDTO.SignIn request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var reference = $"OTP-{Guid.NewGuid():N}";
            var getotp = _otpService.GenerateOtp(reference);
            return Ok(getotp);
        }

        [HttpPost]
        [Route("ConfirmSignIn")]
        public IActionResult ConfirmSignIn([FromBody] RequestDTO.Otp request)
        {
            var CheckOtp = _otpService.CheckOtp(request);
            if (!CheckOtp)
            {
                return BadRequest("รหัส otp ไม่ถูกต้อง");
            }
            return Ok("เข้าสู่ระบบ");
        }
    }
}
