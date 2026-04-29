namespace TaskManagement.UseCases.Projects.DTO
{
    public record IssueDTO(
        IssueContent Content,
        IssueSeverity Severity,
        bool IsResolved,
        IssueResolvedComment? ResolvedComment,
        IReadOnlyList<AttachmentUrl> Attachments);
}
