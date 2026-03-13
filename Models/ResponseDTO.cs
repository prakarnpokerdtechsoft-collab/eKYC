namespace EKYCWebhook.Models
{
    public class ResponseDTO
    {
        public class ReceiveWebhook
        { 
            public bool? succuss { get; set; }
            public string? content { get; set; }
            public string? message { get; set; }
        }

        public class TokenResponse
        {
            public string? access_token { get; set; }
            public int? expires_in { get; set; }
            public string? token_type { get; set; }
        }

        public class CaseCreateResponse
        {
            public string? id { get; set; }
            public DateTime? createdAt { get; set; }
            public DateTime? updatedAt { get; set; }
            public string? status { get; set; }
            public string? citizenId { get; set; }
            public string? insuredFirstName { get; set; }
            public string? insuredLastName { get; set; }
            public string? dateOfBirth { get; set; }
            public string? phoneNumber { get; set; }
            public string? title { get; set; }
        }

        public class ProprietorsResponse
        { 
            public List<verificationsResponse>? verifications { get; set; }
        }

        public class verificationsResponse
        {
            public List<proprietorVerifications>? proprietorVerifications { get; set; }
        }

        public class proprietorVerifications
        { 
            public string? verificationId { get; set; }
        }
    }
}
