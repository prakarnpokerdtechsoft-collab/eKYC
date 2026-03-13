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
                citizenId = "",
                title = "006",
                insuredFirstName = "",
                insuredMiddleName = "",
                insuredLastName = "",
                dateOfBirth = "",
                phoneNumber = "",
                Email = "",
            };

            //เปิด case
            var client = new RestClient("https://portal.mac.appmanteam.com");
            var request = new RestRequest("/api/v3/case-keeper/cases", Method.Post); 

            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddHeader("content-type", "application/json");
            request.AddBody(body);

            var response = await client.ExecuteAsync(request);
            var caseResponse = JsonSerializer.Deserialize<CaseCreateResponse>(response.Content!);
            //var caseId = caseResponse?.id;

            //ส่ง sms ให้ user 
            var proprietorBody = new
            {
                verifications = new[]{
                    new
                    {
                        notifyType = "sms",
                        caseId = caseResponse?.id,
                        citizenId = caseResponse?.citizenId,
                        insuredFirstName = caseResponse?.insuredFirstName,
                        insuredLastName = caseResponse?.insuredLastName,
                        dateOfBirth = caseResponse?.dateOfBirth,
                        phoneNumber = caseResponse?.phoneNumber,
                        title = caseResponse?.title,
                        frontIdCardConfig = new
                        {
                            required = true,
                            attempts = 3,
                            threshold = 0.8,
                            endFlowOnFailure = false,
                            isEditable = true,
                            validations = new[] { "comparison", "recapture", "mugshotFaceDetails" }
                        },
                        livenessConfig = new
                        {
                            required = true,
                            isEditable = true,
                            endFlowOnFailure = false,
                            threshold = 0.8,
                            attempts = 3
                        },
                        faceRecognitionConfig = new
                        {
                            required = true,
                            attempts = 3,
                            threshold = 0.8,
                            endFlowOnFailure = false
                        }
                    }
                }
            };
            var request2 = new RestRequest("/api/v3/case-keeper/proprietors", Method.Post);
            request2.AddHeader("Authorization", $"Bearer {token}");
            request2.AddJsonBody(proprietorBody);
            var response2 = await client.ExecuteAsync(request2);
            var verificationResponse = JsonSerializer.Deserialize<ProprietorsResponse>(response2.Content!);
            //var verificationId = verificationResponse?.verifications?[0].proprietorVerifications?[0].verificationId;

            return Ok(verificationResponse);
        }

        [HttpGet]
        [Route("verifications")] //เช้คสถานะ Verify ที่ส่งให้ user สแกน ocr ผ่าน sms
        public async Task<IActionResult> GetVerifications()
        {
            var verificationsId = "65b02696-3987-42a3-95e6-c82080a44ebd";
            var token = await _ekycService.Checktoken();

            var client = new RestClient("https://portal.mac.appmanteam.com");
            var request = new RestRequest($"/api/v3/case-keeper/verifications/{verificationsId}", Method.Get);
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddHeader("content-type", "application/json");
            var response = await client.ExecuteAsync(request);
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