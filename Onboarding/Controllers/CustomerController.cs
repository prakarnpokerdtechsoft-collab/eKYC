using Microsoft.AspNetCore.Mvc;
using Onboarding.Core.Interfaces.Helper;
using Onboarding.Core.Interfaces.Services;
using Onboarding.Models;
using RestSharp;
using System.Text.Json;
using System.Xml.Linq;

namespace Onboarding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IEkycService _ekycService;
        private readonly IDocumentPipelineService _documentPipelineService;
        private readonly IClientSetting _clientSetting;

        public CustomerController(IEkycService ekycService, IDocumentPipelineService documentPipelineService, IClientSetting clientSetting)
        {
            _ekycService = ekycService;
            _documentPipelineService = documentPipelineService;
            _clientSetting = clientSetting;
        }

        [HttpPost]
        [Route("create_case")]
        public async Task<IActionResult> CreateCase(RequestDTO.Case model)
        {
            var body = new RequestDTO.CreateCase
            {
                citizenId = model.Citizen == null ? "" : model.Citizen,
                title = "006",
                insuredFirstName = model.FirstName == null ? "" : model.FirstName,
                insuredMiddleName = model.MiddleName == null ? "" : model.MiddleName,
                insuredLastName = model.LastName == null ? "" : model.LastName,
                dateOfBirth = model.Year == null ? "" : model.Year + "-" + model.Month + "-" + model.Day,
                phoneNumber = model.Phone,
                Email = model.Email == null ? "" : model.Email,
            };

            var result = await _ekycService.CreateCase(body);
            var verificationResponse = JsonSerializer.Deserialize<ResponseDTO.ProprietorsResponse>(result);
            //var verificationId = verificationResponse?.verifications?[0].proprietorVerifications?[0].verificationId;

            return Ok(verificationResponse);
        }

        [HttpPost]
        [Route("verifications")] //เช้คสถานะ Verify ที่ส่งให้ user สแกน ocr ผ่าน sms
        public async Task<IActionResult> Verifications(RequestDTO.Verifications model)
        {
            var result = await _ekycService.GetVerification(model);
            return Ok(result);
        }

        [HttpPost]
        [Route("create_document")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateDocument([FromForm] RequestDTO.UploadFileDocument model)
        {
           var body = new RequestDTO.CreateDocument
           {
               documentType = "bankStatement",
               fileName = model.FileAccess!.FileName,
               fileSize = model.FileAccess!.Length,
           };

           var result = await _documentPipelineService.CreateDocument(body);
            return Ok(result);
        }


        [HttpGet]
        [Route("bank-statement")]
        public async Task<IActionResult> GetResult(string id)
        { 
            var result = await _documentPipelineService.GetBankStatementResult(id);
            return Ok(result);
        }
    }
}
