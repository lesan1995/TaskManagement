namespace TaskManagement.SharedKernel.File
{
    public interface IFileStorageService
    {
        Task<string> UploadAsync(Stream fileStream, string fileName, string contentType, CancellationToken ct = default);
        Task<bool> DeleteAsync(string fileUrl, CancellationToken ct = default);
        Task<bool> DeletesAsync(IEnumerable<string> fileUrls, CancellationToken ct = default);
        Task<Stream> DownloadAsync(string fileUrl, CancellationToken ct = default);
    }
}
