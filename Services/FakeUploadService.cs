using System.IO;
using System.Threading.Tasks;
namespace UploadFilesServer.Services
{
    public class FakeUploadService : IUploadService
    {
        private readonly string _storageConnectionString;
        private readonly ILogger<FakeUploadService> _logger;
        public FakeUploadService(IConfiguration configuration, ILogger<FakeUploadService> logger)
        {
            _storageConnectionString = configuration.GetConnectionString("BlobConnectionString");
            _logger = logger;
        }
        public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType, string targetContainerName)
        {
            _logger.LogInformation("storageConnectionString is: " + _storageConnectionString);
            _logger.LogInformation("Upload Handled by FakeUploadService");
            try {
                var filePath = Path.Combine("/photos/", targetContainerName, fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    await fileStream.CopyToAsync(stream);
                }
                return "localFile/photos/" + targetContainerName + "/" + fileName;
            } catch (Exception ex) {
                _logger.LogError("Failed to write to disk");
                _logger.LogError(ex.Message);
                throw ex;
            }
        }
    }
}