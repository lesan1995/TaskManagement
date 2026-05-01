using Microsoft.Extensions.Logging;
using TaskManagement.SharedKernel.File;

namespace TaskManagement.UseCases.Projects.Task.Remove
{
    public class RemoveTaskHandler(
        IRepository<Project> repository,
        ICurrentUserService currentUser,
        IFileStorageService fileService,
        IUnitOfWork unitOfWork,
        ILogger<RemoveTaskHandler> logger)
        : ICommandHandler<RemoveTaskCommand, Result>
    {
        public async ValueTask<Result> Handle(RemoveTaskCommand command, CancellationToken ct)
        {
            var spec = new ProjectByIdWithTaskSpec(command.ProjectId);
            var project = await repository.FirstOrDefaultAsync(spec, ct);
            if (project == null)
                return Result.NotFound();

            bool hasPermission = currentUser.IsManager || project.IsProjectManager(currentUser.UserId);
            if (!hasPermission)
                return Result<MarkTaskResultDTO>.Forbidden("You do not have permission to remove tasks");

            project.RemoveTask(command.TaskItemId);
            
            project.SetModified(currentUser.UserId.ToString());

            await unitOfWork.BeginTransactionAsync(ct);

            try
            {
                await repository.UpdateAsync(project, ct);
                await fileService.DeletesAsync(project.GetTaskAttachmentUrls(command.TaskItemId), ct);
                await unitOfWork.CommitAsync(ct);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                await unitOfWork.RollBackAsync(ct);
                return Result.Error("Cannot remove task");
            }
            return Result.Success();
        }
    }
}
