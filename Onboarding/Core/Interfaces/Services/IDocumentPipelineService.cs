namespace Onboarding.Core.Interfaces.Services
{
    public interface IDocumentPipelineService
    {
        public Task<string> GetBankStatementResult(string bankStatementId);
    }
}
