using Microsoft.Extensions.Logging;
using TaskManagement.SharedKernel.File;

namespace TaskManagement.UseCases.Projects.Task.RemoveAttachment
{
    public class RemoveTaskAttachmentHandler(
        IRepository<Project> repository,
        ICurrentUserService currentUser,
        IUserService userService,
        IFileStorageService fileStorage,
        IUnitOfWork unitOfWork,
        ILogger<RemoveTaskAttachmentHandler> logger)
        : ICommandHandler<RemoveTaskAttachmentCommand, Result<TaskItemDTO>>
    {
        public async ValueTask<Result<TaskItemDTO>> Handle(RemoveTaskAttachmentCommand command, CancellationToken ct)
        {
            var spec = new ProjectByIdWithTaskSpec(command.ProjectId);
            var project = await repository.FirstOrDefaultAsync(spec, ct);
            if (project == null)
                return Result<TaskItemDTO>.NotFound();

            bool hasPermission = currentUser.IsManager || project.IsProjectManager(currentUser.UserId)
                || project.IsTaskOwner(command.TaskItemId, currentUser.UserId);
            if (!hasPermission)
                return Result<TaskItemDTO>.Forbidden("You do not have permission to remove attachment on this task");

            var task = project.RemoveTaskAttachment(command.TaskItemId, command.FileUrl);

            UserInfo? assignUserInfo = task.AssigneeId.HasValue
                    ? await userService.GetUserAsync(task.AssigneeId.Value, ct)
                    : null;

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
                return Result<TaskItemDTO>.Error($"An error occurred when remove task attachment: {command.FileUrl}");
            }

            return Result<TaskItemDTO>.Success(task.MapToTaskItemDto(assignUserInfo));
        }
    }
}
