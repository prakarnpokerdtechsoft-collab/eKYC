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
    }
}
