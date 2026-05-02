using Microsoft.Extensions.Logging;
using TaskManagement.SharedKernel.File;

namespace TaskManagement.UseCases.Projects.Issuee.RemoveAttachment
{
    public class RemoveIssueAttachmentHandler(
        IRepository<Project> repository,
        ICurrentUserService currentUser,
        IFileStorageService fileStorage,
        IUnitOfWork unitOfWork,
        ILogger<RemoveIssueAttachmentHandler> logger)
        : ICommandHandler<RemoveIssueAttachmentCommand, Result<IssueDTO>>
    {
        public async ValueTask<Result<IssueDTO>> Handle(RemoveIssueAttachmentCommand command, CancellationToken ct)
        {
            var spec = new ProjectByIdWithIssueSpec(command.ProjectId);
            var project = await repository.FirstOrDefaultAsync(spec, ct);
            if (project == null)
                return Result<IssueDTO>.NotFound();

            bool hasPermission = project.IsProjectManager(currentUser.UserId);
            if (!hasPermission)
                return Result<IssueDTO>.Forbidden("You do not have permission to remove attachment on this issue");

            var issue = project.RemoveIssueAttachment(command.IssueId, command.FileUrl);

            project.SetModified(currentUser.UserId.ToString());

            await unitOfWork.BeginTransactionAsync();

            try
            {
                await repository.UpdateAsync(project, ct);

                await fileStorage.DeleteAsync(command.FileUrl.ToString(), ct);

                await unitOfWork.CommitAsync(ct);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                await unitOfWork.RollbackAsync(ct);
                return Result<IssueDTO>.Error($"An error occurred when remove task attachment: {command.FileUrl}");
            }

            return Result<IssueDTO>.Success(issue.MapToIssueDto());
        }
    }
}
