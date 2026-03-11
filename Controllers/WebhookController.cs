using EKYCWebhook.Entity;
using EKYCWebhook.Models;
using EKYCWebhook.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestSharp;

namespace EKYCWebhook.Controllers
{
    [ApiController]
    [Route("webhook/ekyc")]
    public class WebhookController : ControllerBase
    {
        private readonly ILogger<WebhookController> _logger;
        private readonly EkycService _ekycService;
        private readonly EKYCWebhookContext _context;

        public WebhookController(
            ILogger<WebhookController> logger,
            EkycService ekycService,
            EKYCWebhookContext context)
        {
            _logger = logger;
            _ekycService = ekycService;
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> ReceiveWebhook([FromBody] WebhookEvent request)
        {
            ResponseDTO.ReceiveWebhook receiveWebhook = new ResponseDTO.ReceiveWebhook();
            if (request.key == "verification.verified")
            {
                var result = await _ekycService.GetVerification(request.dataId!);
                receiveWebhook.succuss = true;
                receiveWebhook.content = result;
                receiveWebhook.message = "succuss";
            }
            return Ok(receiveWebhook);
        }
    }
}