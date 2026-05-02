namespace TaskManagement.UseCases.Projects.Issuee.AddAttachment
{
    public record AddIssueAttachmentCommand(ProjectId ProjectId, IssueId IssueId, AttachmentRequestDTO Attachment) : ICommand<Result<IssueDTO>>;
}