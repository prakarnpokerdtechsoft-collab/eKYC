using NuGet.Common;
using Onboarding.Core.Interfaces.Helper;
using Onboarding.Core.Interfaces.Services;
using Onboarding.Models;
using RestSharp;
using System.Text.Json;

namespace Onboarding.Infrastructure.Services.External
{
    public class EkycService : IEkycService
    {
        private readonly IConfiguration _config;
        private readonly IClientSetting _clientSetting;

        public EkycService(IClientSetting clientSetting, IConfiguration config)
        {
            _clientSetting = clientSetting;
            _config = config;
        }

        public async Task<string> CreateCase(RequestDTO.CreateCase body)
        {
            var token = await _clientSetting.Checktoken("Appmen:Ekyc");

            //เปิด case
            var client = new RestClient(_config.GetSection("Appmen:BaseUrl").Value!);
            var request = new RestRequest("/api/v3/case-keeper/cases", RestSharp.Method.Post);

            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddHeader("content-type", "application/json");
            request.AddBody(body);

            var response = await client.ExecuteAsync(request);
            var caseResponse = JsonSerializer.Deserialize<ResponseDTO.CaseCreateResponse>(response.Content!);
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
            var request2 = new RestRequest("/api/v3/case-keeper/proprietors", RestSharp.Method.Post);
            request2.AddHeader("Authorization", $"Bearer {token}");
            request2.AddJsonBody(proprietorBody);
            var response2 = await client.ExecuteAsync(request2);
            return response2.Content!;
        }

        public async Task<string> GetVerification(RequestDTO.Verifications model)
        {
            var token = await _clientSetting.Checktoken("Appmen:Ekyc");
            var client = new RestClient("https://portal.mac.appmanteam.com");
            var request = new RestRequest($"/api/v3/case-keeper/verifications/{model.verificationsId}", RestSharp.Method.Get);
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddHeader("content-type", "application/json");
            var response = await client.ExecuteAsync(request);
            return response.Content!;
        }
    }
}