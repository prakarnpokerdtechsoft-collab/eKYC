namespace Onboarding.Core.Interfaces.Services
{
    public interface IMinioService
    {
        Task<string> UploadAsync(string bucket, string objectName, Stream data, string contentType);
        Task<Stream> GetAsync(string bucket, string objectName);
        Task DeleteAsync(string bucket, string objectName);
        Task<bool> ExistsAsync(string bucket, string objectName);
        Task<string> GetPresignedUrlAsync(string bucket, string objectName, int expirySeconds);
    }
}
