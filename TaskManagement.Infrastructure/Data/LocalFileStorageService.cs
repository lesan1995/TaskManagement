using Microsoft.Extensions.Configuration;
using TaskManagement.SharedKernel.File;

namespace TaskManagement.Infrastructure.Data
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly string _basePath;
        private readonly string _baseUrl;
        public LocalFileStorageService(IConfiguration configuration)
        {
            _basePath = configuration["FileStorage:LocalPath"] ?? "wwwroot/uploads";
            _baseUrl = configuration["FileStorage:BaseUrl"] ?? "/uploads";

            if (!Directory.Exists(_basePath))
                Directory.CreateDirectory(_basePath);
        }

        public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType, CancellationToken ct = default)
        {
            var uniqueFileName = $"{Guid.NewGuid}_{fileName}";
            var fullPath = Path.Combine(_basePath, uniqueFileName);

            await using var fs = new FileStream(fullPath, FileMode.CreateNew);
            await fileStream.CopyToAsync(fs);

            return uniqueFileName.Replace("\\", "/");

        }

        public Task<Stream> DownloadAsync(string filePath, CancellationToken ct = default)
        {
            var fullPath = Path.Combine(_basePath, filePath);
            if (!File.Exists(fullPath))
                throw new FileNotFoundException("File not found", filePath);

            return Task.FromResult<Stream>(File.OpenRead(fullPath));
        }

        public Task<bool> DeleteAsync(string filePath, CancellationToken ct = default)
        {
            var fullPath = Path.Combine(_basePath, filePath);
            if (!File.Exists(fullPath)) return Task.FromResult(false);

            File.Delete(fullPath);
            return Task.FromResult(true);
        }

        public Task<bool> DeletesAsync(IEnumerable<string> filePaths, CancellationToken ct = default)
        {
            foreach(var filePath in filePaths)
            {
                var fullPath = Path.Combine(_basePath, filePath);
                if (!File.Exists(fullPath)) return Task.FromResult(false);
            }

            foreach (var filePath in filePaths)
            {
                var fullPath = Path.Combine(_basePath, filePath);
                File.Delete(fullPath);
            }

            return Task.FromResult(true);
        }
    }
}
