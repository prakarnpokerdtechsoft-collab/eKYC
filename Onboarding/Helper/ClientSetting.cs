using Onboarding.Core.Interfaces.Helper;
using Onboarding.Models;
using System.Text.Json;

namespace Onboarding.Helper
{
    public class ClientSetting : IClientSetting
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private IHttpContextAccessor _accessor;

        public ClientSetting(IConfiguration config, HttpClient httpClient, IHttpContextAccessor accessor)
        {
            _config = config;
            _httpClient = httpClient;
            _accessor = accessor;
        }

        public async Task<Dictionary<string, string>> ClientBody(string Aceess)
        {
            var GetSection = _config.GetSection(Aceess);
            var body = new Dictionary<string, string>
            {
                { "grant_type", GetSection["grant_type"]! },
                { "client_id", GetSection["client_id"]! },
                { "client_secret", GetSection["client_secret"]! }
            };
            return body;
        }

        public async Task<string> GetAccessToken(string Aceess)
        {
            var values = await ClientBody(Aceess);
            var content = new FormUrlEncodedContent(values);
            var response = await _httpClient.PostAsync(
                $"{_config["Appmen:BaseUrl"]}/auth/realms/mac-portal/protocol/openid-connect/token",
                content
            );

            var json = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<ResponseDTO.TokenResponse>(json);
            var token = tokenResponse!.access_token;
            return token!;
        }

        public async Task<string> Checktoken(string Aceess)
        {
            var gettoken = _accessor?.HttpContext?.Session.GetString("appmentoken");
            if (String.IsNullOrEmpty(gettoken))
            {
                gettoken = await GetAccessToken(Aceess);
                _accessor?.HttpContext?.Session.SetString("appmentoken", gettoken!);
            }
            return gettoken!;
        }
    }
}
