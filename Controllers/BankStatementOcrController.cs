using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/ocr/bank-statement")]
public class BankStatementOcrController : ControllerBase
{
    private readonly DocumentPipelineService _service;

    public BankStatementOcrController(DocumentPipelineService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetResult(string id)
    {
        var result = await _service.GetBankStatementResult(id);
        return Ok(result);
    }
}