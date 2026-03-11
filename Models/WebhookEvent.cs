namespace EKYCWebhook.Models
{
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