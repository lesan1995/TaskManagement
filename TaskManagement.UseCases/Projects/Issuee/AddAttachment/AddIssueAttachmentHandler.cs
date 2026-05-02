using TaskManagement.SharedKernel.File;

namespace TaskManagement.UseCases.Projects.Issuee.AddAttachment
{
    public class AddIssueAttachmentHandler(
        IRepository<Project> repository,
        ICurrentUserService currentUser,
        IFileStorageService fileStorage)
        : ICommandHandler<AddIssueAttachmentCommand, Result<IssueDTO>>
    {
        public async ValueTask<Result<IssueDTO>> Handle(AddIssueAttachmentCommand command, CancellationToken ct)
        {
            var spec = new ProjectByIdWithIssueSpec(command.ProjectId);
            var project = await repository.FirstOrDefaultAsync(spec, ct);
            if (project == null)
                return Result<IssueDTO>.NotFound();

            bool hasPermission = project.IsProjectManager(currentUser.UserId);
            if (!hasPermission)
                return Result<IssueDTO>.Forbidden("You do not have permission to upload attachment on this issue");

            var fileUrl = await fileStorage.UploadAsync(command.Attachment.FileStream, command.Attachment.FileName, command.Attachment.ContentType, ct);

            var issue = project.AddIssueAttachment(command.IssueId, AttachmentUrl.Create(fileUrl), currentUser.UserId);
            
            project.SetModified(currentUser.UserId.ToString());

            await repository.UpdateAsync(project, ct);

            return Result<IssueDTO>.Success(issue.MapToIssueDto());
        }
    }
}
