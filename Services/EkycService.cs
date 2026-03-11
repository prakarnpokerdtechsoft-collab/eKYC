using EKYCWebhook.Models;
using RestSharp;
using System.Net.Http.Headers;
using System.Text.Json;

namespace EKYCWebhook.Services
{
    public class EkycService
    {
        private readonly HttpClient _httpClient;
        private string _token;
        private DateTime _tokenExpire;

        public EkycService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetAccessToken()
        {
            if (!string.IsNullOrEmpty(_token) && DateTime.UtcNow < _tokenExpire)
            {
                return _token;
            }

            var values = new Dictionary<string, string>
            {
                { "client_id", "techsoft-ekyc-demo-case-keeper-service-account" },
                { "client_secret", "8HxlaqBMaHQAJaglU3mgQ2fXOPJU7t9T" },
                { "grant_type", "client_credentials" }
            };

            var content = new FormUrlEncodedContent(values);

            var response = await _httpClient.PostAsync(
                "https://mac-portal.appmanteam.com/auth/realms/mac-portal/protocol/openid-connect/token",
                content
            );

            var json = await response.Content.ReadAsStringAsync();

            var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(json);

            _token = tokenResponse!.access_token;
            _tokenExpire = DateTime.UtcNow.AddSeconds(tokenResponse.expires_in - 60);

            return _token;
        }

        public async Task<string> GetVerification(string verificationId)
        {
            var token = await GetAccessToken();

            var options = new RestClientOptions("https://mac-portal.appmanteam.com/api/v3/case-keeper/proprietor-verifications");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {token}");
            var response = await client.GetAsync(request);

            return response.Content!;

            //var options2 = new RestClientOptions("https://mac-portal.appmanteam.com");
            //var client2 = new RestClient(options2);
            //var request2 = new RestRequest(
            //    $"/api/v3/case-keeper/verifications/{verificationId}",
            //    Method.Get);
            //request2.AddHeader("accept", "application/json");
            //request2.AddHeader("Authorization", $"Bearer {token}");
            //var response2 = await client2.ExecuteAsync(request2);
            //return response2.Content!;
        }
    }
}