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
            BlobContainerClient container = DeclareBlobContainer(targetContainerName);

            await FetchOrBuildContainer(container);

            BlobClient blob = await DeleteExistingBlobOfNameIfAny(fileName, container);
            
            try {
                await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = contentType });
                return blob.Uri.ToString();
            } catch {
                _logger.LogError("Failed to Upload Image");
                throw;
            }
        }

        private async Task<BlobClient> DeleteExistingBlobOfNameIfAny(string fileName, BlobContainerClient container)
        {
            try {
                var blob = container.GetBlobClient(fileName);
                await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
                return blob;
            } catch (Exception ex) {
                _logger.LogError("Failed in DeleteExistingBlobOfNameIfAny");
                throw;
            }
        }

        private BlobContainerClient DeclareBlobContainer(string targetContainerName)
        {
            try {
                var container = new BlobContainerClient(_storageConnectionString, targetContainerName);
                return container;
            } catch (Exception ex) {
                _logger.LogError("BlobConnectionString: " + _storageConnectionString);
                _logger.LogError("targetContainerName:" + targetContainerName);
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        private async Task FetchOrBuildContainer(BlobContainerClient container)
        {
            try {
                var createResponse = await container.CreateIfNotExistsAsync();

                if (createResponse != null && createResponse.GetRawResponse().Status == 201)
                    await container.SetAccessPolicyAsync(PublicAccessType.Blob);
            } catch (Exception ex) {
                _logger.LogError("Failed in FetchOrBuildContainer");
                _logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}