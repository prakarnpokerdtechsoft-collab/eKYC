namespace Onboarding.Core.Interfaces.Helper
{
    public interface IClientSetting
    {
        public Task<Dictionary<string, string>> ClientBody(string Aceess);
        public Task<string> GetAccessToken(string Aceess);
        public Task<string> Checktoken(string Aceess);
    }
}
