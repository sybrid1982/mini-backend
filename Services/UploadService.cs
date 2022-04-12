using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
namespace UploadFilesServer.Services
{
    public class UploadService : IUploadService
    {
        private readonly string _storageConnectionString;
        private readonly ILogger<UploadService> _logger;
        public UploadService(IConfiguration configuration, ILogger<UploadService> logger)
        {
            _storageConnectionString = configuration.GetConnectionString("BlobConnectionString");
            _logger = logger;
        }
        public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType, string targetContainerName)
        {
            try {
                _logger.LogInformation("Setting container to new BlobContainerClient");
                _logger.LogInformation("BlobConnectionString: " + _storageConnectionString);
                var container = new BlobContainerClient(_storageConnectionString, targetContainerName);
                _logger.LogInformation("Attempting to create " + targetContainerName + " if it does not exist");
                var createResponse = await container.CreateIfNotExistsAsync();
                _logger.LogInformation("createResponse ClientRequestId " + createResponse.GetRawResponse().ClientRequestId);
                _logger.LogInformation("createResponse Status " + createResponse.GetRawResponse().Status);
                _logger.LogInformation("createResponse Content " + createResponse.GetRawResponse().Content);

                if (createResponse != null && createResponse.GetRawResponse().Status == 201)
                    await container.SetAccessPolicyAsync(PublicAccessType.Blob);

                _logger.LogInformation("Getting Blob Client for " + fileName);
                var blob = container.GetBlobClient(fileName);
                await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
                _logger.LogInformation("Attempting upload to blob " + blob.BlobContainerName);

                await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = contentType });
                return blob.Uri.ToString();

            } catch (Exception ex) {
                _logger.LogError("Failed to connect to blob container");
                _logger.LogError(ex.Message);
                throw ex;
            }
        }
    }
}