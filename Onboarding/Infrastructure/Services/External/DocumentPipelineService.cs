using Microsoft.AspNetCore.Mvc;
using Onboarding.Core.Interfaces.Helper;
using Onboarding.Core.Interfaces.Services;
using Onboarding.Models;
using RestSharp;
using System.Net.Http.Headers;
using System.Xml.Linq;

public class DocumentPipelineService : IDocumentPipelineService
{
    private readonly IConfiguration _config;
    private readonly IClientSetting _clientSetting;

    public DocumentPipelineService(IConfiguration config, IClientSetting clientSetting)
    {
        _config = config;
        _clientSetting = clientSetting;
    }

    public async Task<IActionResult> CreateDocument(RequestDTO.CreateDocument body)
    {
        var token = await _clientSetting.Checktoken("Appmen:DocumentPipeline");

        var client = new RestClient(_config.GetSection("Appmen:BaseUrl").Value!);

        var request = new RestRequest("/api/v1/document-pipeline/documents", Method.Post);
        request.AddHeader("accept", "application/json");
        request.AddHeader("Authorization", $"Bearer {token}");
        request.AddHeader("content-type", "application/json");
        request.AddBody(body);
        var response = await client.PostAsync(request);

        var groupbody = new
        {
            documentType = "bankStatement",
            documents = response.Content
        };

        //var request2 = new RestRequest("/api/v1/document-pipeline/documentGroups");
        //request2.AddHeader("accept", "application/json");
        //request2.AddBody(groupbody);
        //var response2 = await client.PostAsync(request2);

        //Console.WriteLine("{0}", response.Content);



        ////var request2 = new RestRequest("/api/v1/document-pipeline/documentGroups", Method.Post);
        ////request2.AddHeader("accept", "application/json");
        ////request2.AddHeader("Authorization", $"Bearer {token}");
        ////request2.AddHeader("content-type", "application/json");
        ////request2.AddBody(body);
        ////var response2 = await client.PostAsync(request2);

        //Console.WriteLine("{0}", response2.Content);





        return new JsonResult(response.Content);
    }

    public async Task<string> GetBankStatementResult(string bankStatementId)
    {
        var token = await _clientSetting.Checktoken("Appmen:DocumentPipeline");
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var url = $"{_config["Appmen:BaseUrl"]}/api/v1/document-pipeline/bankStatements/{bankStatementId}";
        var response = await client.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }
}