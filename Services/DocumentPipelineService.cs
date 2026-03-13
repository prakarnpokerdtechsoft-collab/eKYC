using System.Text.Json;
using System.Net.Http.Headers;

public class DocumentPipelineService
{
    private readonly IConfiguration _config;

    public DocumentPipelineService(IConfiguration config)
    {
        _config = config;
    }

    public async Task<string> GetBankStatementResult(string bankStatementId)
    {
        var token = await GetToken();

        var client = new HttpClient();

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var url = $"{_config["DocumentPipeline:BaseUrl"]}/api/v1/document-pipeline/bankStatements/{bankStatementId}";

        var response = await client.GetAsync(url);

        return await response.Content.ReadAsStringAsync();
    }

    private async Task<string> GetToken()
    {
        var client = new HttpClient();

        var body = new Dictionary<string, string>
        {
            { "grant_type", "client_credentials" },
            { "client_id", _config["DocumentPipeline:ClientId"] },
            { "client_secret", _config["DocumentPipeline:ClientSecret"] }
        };

        var response = await client.PostAsync(
            _config["DocumentPipeline:TokenUrl"],
            new FormUrlEncodedContent(body));

        var json = await response.Content.ReadAsStringAsync();

        var obj = JsonSerializer.Deserialize<JsonElement>(json);

        return obj.GetProperty("access_token").GetString();
    }
}