using Microsoft.AspNetCore.Mvc;
using Onboarding.Models;

namespace Onboarding.Core.Interfaces.Services
{
    public interface IDocumentPipelineService
    {
        public Task<IActionResult> CreateDocument(RequestDTO.CreateDocument model);
        public Task<string> GetBankStatementResult(string bankStatementId);
    }
}
