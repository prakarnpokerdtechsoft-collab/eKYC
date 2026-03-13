using EKYCWebhook.Entity;
using EKYCWebhook.Entity.Data;
using EKYCWebhook.Models;
using EKYCWebhook.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EKYCWebhook.Controllers
{
    [ApiController]
    [Route("api/ocr/bank-statement")]
    public class BankStatementOcrController : ControllerBase
    {
        private readonly DocumentPipelineService _service;
        private readonly EKYCWebhookContext _context;

        public BankStatementOcrController(
            DocumentPipelineService service,
            EKYCWebhookContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetResult(string id)
        {
            var result = await _service.GetBankStatementResult(id);
            var ConvertResult = System.Text.Json.JsonSerializer.Deserialize<BankStatementResult>(result); //JsonSerializer.



            if (result == null)
                return NotFound();

            var entity = new OcrBankStatement()
            {
                BankName = ConvertResult?.result?.bankNameTH!,
                Id = Guid.Parse(ConvertResult?.id!),
                AccountName = "",
                AccountNumber = "",
                CreatedAt = DateTime.Now
            };

            _context.OcrBankStatement.Add(entity);
            await _context.SaveChangesAsync();

            return Ok(result);
        }
    }
}