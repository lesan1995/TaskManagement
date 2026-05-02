using Microsoft.Extensions.Logging;
using TaskManagement.SharedKernel.File;

namespace TaskManagement.UseCases.Projects.Issuee.Remove
{
    public class RemoveIssueHandler(
        IRepository<Project> repository,
        ICurrentUserService currentUser,
        IFileStorageService fileService,
        IUnitOfWork unitOfWork,
        ILogger<RemoveIssueHandler> logger)
        : ICommandHandler<RemoveIssueCommand, Result>
    {
        public async ValueTask<Result> Handle(RemoveIssueCommand command, CancellationToken ct)
        {
            var spec = new ProjectByIdWithIssueSpec(command.ProjectId);
            var project = await repository.FirstOrDefaultAsync(spec, ct);
            if (project == null)
                return Result.NotFound();

            bool hasPermission = project.IsProjectManager(currentUser.UserId);
            if (!hasPermission)
                return Result.Forbidden("You do not have permission to remove issues");

            project.RemoveIssue(command.IssueId);
            
            project.SetModified(currentUser.UserId.ToString());

            await unitOfWork.BeginTransactionAsync(ct);

            try
            {
                await repository.UpdateAsync(project, ct);
                await fileService.DeletesAsync(project.GetIssueAttachmentUrls(command.IssueId), ct);
                await unitOfWork.CommitAsync(ct);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                await unitOfWork.RollbackAsync(ct);
                return Result.Error("Cannot remove issue");
            }
            return Result.Success();
        }
    }
}
