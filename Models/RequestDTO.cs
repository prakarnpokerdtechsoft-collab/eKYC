namespace EKYCWebhook.Models
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


        public class WebhookEvent
        {
            public string? key { get; set; }
            public string? dataId { get; set; }
            public WebhookData? Data { get; set; }
        }

        public class WebhookData
        {
            public string? status { get; set; }
        }
    }
}
