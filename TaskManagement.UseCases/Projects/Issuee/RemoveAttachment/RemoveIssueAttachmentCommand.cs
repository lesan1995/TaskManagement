namespace TaskManagement.UseCases.Projects.Issuee.RemoveAttachment
{
    public record RemoveIssueAttachmentCommand(ProjectId ProjectId, IssueId IssueId, AttachmentUrl FileUrl) : ICommand<Result<IssueDTO>>;
}