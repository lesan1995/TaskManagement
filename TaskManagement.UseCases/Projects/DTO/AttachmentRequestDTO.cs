namespace TaskManagement.UseCases.Projects.DTO
{
    public record AttachmentRequestDTO(Stream FileStream, string FileName, string ContentType);
}
