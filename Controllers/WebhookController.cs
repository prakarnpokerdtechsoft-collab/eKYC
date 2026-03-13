using Azure.Core;
using EKYCWebhook.Entity;
using EKYCWebhook.Models;
using EKYCWebhook.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using System.Numerics;
using System.Text.Json;
using static EKYCWebhook.Models.RequestDTO;
using static EKYCWebhook.Models.ResponseDTO;
using static System.Net.Mime.MediaTypeNames;

namespace EKYCWebhook.Controllers
{
    [ApiController]
    [Route("/api/webhook")]
    public class WebhookController : ControllerBase
    {
        private readonly ILogger<WebhookController> _logger;
        private readonly EkycService _ekycService;

        public WebhookController(
            ILogger<WebhookController> logger,
            EkycService ekycService)
        {
            _logger = logger;
            _ekycService = ekycService;
        }

        [HttpPost]
        [Route("create_case")]
        public async Task<IActionResult> CreateCase(RequestDTO.Case model)
        {
            var token = await _ekycService.Checktoken();

            var body = new
            {
                citizenId = "1959800111204",
                title = "006",
                insuredFirstName = "กิตติพงศ์ แซ่เลี้ยง",
                insuredMiddleName = "",
                insuredLastName = "แซ่เลี้ยง",
                dateOfBirth = "1996-07-15",
                phoneNumber = "0632409777",
                Email = "",
            };

            var options = new RestClientOptions("https://portal.mac.appmanteam.com/api/v3/case-keeper/cases"); //เช็คจาก dashboard ของ appmen อันเดียวกัน
            var client = new RestClient(options);
            var request = new RestRequest("");

            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddHeader("content-type", "application/json");
            request.AddBody(body);

            var response = await client.PostAsync(request);
            //var caseResponse = JsonSerializer.Deserialize<CaseCreateResponse>(response.Content!);
            //var caseId = caseResponse!.id;


            //var proprietorBody = new
            //{
            //    caseId = caseResponse!.id,
            //    title = caseResponse.title,
            //    firstName = caseResponse.insuredFirstName,
            //    middleName = "",
            //    lastName = caseResponse.insuredLastName,
            //    citizenId = caseResponse.citizenId,
            //    phoneNumber = caseResponse.phoneNumber,
            //    dateOfBirth = caseResponse.dateOfBirth
            //};

            //var request2 = new RestRequest("https://portal.mac.appmanteam.com/api/v3/case-keeper/proprietors", Method.Post);
            ////https://portal.mac.appmanteam.com/api/v3/case-keeper/proprietor-verifications?limit=30&page=1&order=createdAt-DESC&proprietorId-%24not-%24isNull=&verificationId-%24not-%24isNull=&verification-verificationId-%24isNull=

            //request2.AddHeader("Authorization", $"Bearer {token}");
            //request2.AddJsonBody(proprietorBody);

            //var response2 = await client.ExecuteAsync(request2);


            return Ok(response.Content);
        }

        [HttpPost]
        [Route("ekyc")]
        public async Task<IActionResult> ReceiveWebhook([FromBody] RequestDTO.WebhookEvent request)
        {
            ResponseDTO.ReceiveWebhook receiveWebhook = new ResponseDTO.ReceiveWebhook();
            if (request.key == "verification.verified")
            {
                _logger.LogInformation("dataId = " + request.dataId!);
                var result = await _ekycService.GetVerification(request.dataId!);
                receiveWebhook.succuss = true;
                receiveWebhook.content = result;
                receiveWebhook.message = "succuss";
                _logger.LogInformation(result);
            }
            return Ok(request.dataId);
        }
    }
}