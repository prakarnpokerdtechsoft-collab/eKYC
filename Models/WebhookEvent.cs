namespace EKYCWebhook.Models
{
    public class WebhookEvent
    {
        public string Key { get; set; }
        public WebhookData Data { get; set; }
    }

    public class WebhookData
    {
        public string Id { get; set; }
    }
}