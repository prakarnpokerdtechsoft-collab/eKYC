using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Onboarding.Models
{
    public class RequestDTO
    {
        public class Case
        {
            public string? Citizen { get; set; }
            public string? Title { get; set; }
            public string? FirstName { get; set; }
            public string? MiddleName { get; set; }
            public string? LastName { get; set; }
            public string? Day { get; set; }
            public string? Month { get; set; }
            public string? Year { get; set; }
            public string? Phone { get; set; }
            public string? Email { get; set; }
        }

        public class CreateCase
        { 
            public string? citizenId { get; set; }
            public string? title { get; set; }
            public string? insuredFirstName { get; set; }
            public string? insuredMiddleName { get; set; }
            public string? insuredLastName { get; set; }
            public string? dateOfBirth { get; set; }
            public string? phoneNumber { get; set; }
            public string? Email { get; set; }
        }

        public class Verifications
        { 
            public string? verificationsId { get; set; }
        }

        public class SignUp
        {
            [JsonPropertyName("mobileNo")]
            [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "เบอร์โทรศัพท์ต้องเป็นตัวเลข 10 หลัก")]
            public string? MobileNo { get; set; }
        }

        public class SignIn
        {
            [JsonPropertyName("mobileNo")]
            [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "เบอร์โทรศัพท์ต้องเป็นตัวเลข 10 หลัก")]
            public string? MobileNo { get; set; }
        }

        public class Otp
        {
            [JsonPropertyName("otp")]
            [RegularExpression(@"^[0-9]{6}$", ErrorMessage = "otpต้องเป็นตัวเลข 6 หลัก")]
            public string? OTP { get; set; }

            [JsonPropertyName("reference")]
            public string? Reference { get; set; }
        }

        public class UploadFileDocument
        { 
            public IFormFile? FileAccess { get; set; }
        }

        public class CreateDocument
        { 
            public string? documentType { get; set; }
            public string? fileName { get; set; }
            public long? fileSize { get; set; }
        }
    }
}
