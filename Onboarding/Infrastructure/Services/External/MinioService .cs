using Minio;
using Minio.DataModel.Args;
using Onboarding.Core.Interfaces.Services;

namespace Onboarding.Infrastructure.Services.External
{
    public class MinioService : IMinioService
    {
        private readonly IMinioClient _client;

        public MinioService(IConfiguration config)
        {
            _client = new MinioClient()
                .WithEndpoint(config["Minio:Endpoint"])
                .WithCredentials(
                    config["Minio:AccessKey"],
                    config["Minio:SecretKey"])
                .Build();
        }

        public async Task<string> UploadAsync(string bucket, string objectName, Stream data, string contentType)
        {
            var found = await _client.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucket));

            if (!found)
            {
                await _client.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucket));
            }

            await _client.PutObjectAsync(new PutObjectArgs()
                .WithBucket(bucket)
                .WithObject(objectName)
                .WithStreamData(data)
                .WithObjectSize(data.CanSeek ? data.Length : -1)
                .WithContentType(contentType));

            // return object path (หรือจะ compose URL เอง)
            return objectName;
        }

        public async Task<Stream> GetAsync(string bucket, string objectName)
        {
            var ms = new MemoryStream();

            await _client.GetObjectAsync(new GetObjectArgs()
                .WithBucket(bucket)
                .WithObject(objectName)
                .WithCallbackStream(stream => stream.CopyTo(ms)));

            ms.Position = 0;
            return ms;
        }

        public async Task DeleteAsync(string bucket, string objectName)
        {
            await _client.RemoveObjectAsync(new RemoveObjectArgs()
                .WithBucket(bucket)
                .WithObject(objectName));
        }

        public async Task<bool> ExistsAsync(string bucket, string objectName)
        {
            try
            {
                await _client.StatObjectAsync(new StatObjectArgs()
                    .WithBucket(bucket)
                    .WithObject(objectName));

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> GetPresignedUrlAsync(string bucket, string objectName, int expirySeconds)
        {
            return await _client.PresignedGetObjectAsync(new PresignedGetObjectArgs()
                .WithBucket(bucket)
                .WithObject(objectName)
                .WithExpiry(expirySeconds));
        }
    }
}
