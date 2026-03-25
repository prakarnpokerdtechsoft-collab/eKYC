using Onboarding.Core.Interfaces.Helper;
using Onboarding.Core.Interfaces.Services;
using System.Net.Http.Headers;

public class DocumentPipelineService : IDocumentPipelineService
{
    private readonly IConfiguration _config;
    private readonly IClientSetting _clientSetting;

    public DocumentPipelineService(IConfiguration config, IClientSetting clientSetting)
    {
        _config = config;
        _clientSetting = clientSetting;
    }

    public async Task<string> GetBankStatementResult(string bankStatementId)
    {
        var token = await _clientSetting.GetAccessToken("Appmen:DocumentPipeline");
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var url = $"{_config["Appmen:BaseUrl"]}/api/v1/document-pipeline/bankStatements/{bankStatementId}";
        var response = await client.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }
}