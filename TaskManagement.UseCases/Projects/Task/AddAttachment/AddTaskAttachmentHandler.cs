using TaskManagement.SharedKernel.File;

namespace TaskManagement.UseCases.Projects.Task.AddAttachment
{
    public class AddTaskAttachmentHandler(
        IRepository<Project> repository,
        ICurrentUserService currentUser,
        IUserService userService,
        IFileStorageService fileStorage)
        : ICommandHandler<AddTaskAttachmentCommand, Result<TaskItemDTO>>
    {
        public async ValueTask<Result<TaskItemDTO>> Handle(AddTaskAttachmentCommand command, CancellationToken ct)
        {
            var spec = new ProjectByIdWithTaskSpec(command.ProjectId);
            var project = await repository.FirstOrDefaultAsync(spec, ct);
            if (project == null)
                return Result<TaskItemDTO>.NotFound();

            bool hasPermission = currentUser.IsManager || project.IsProjectManager(currentUser.UserId) 
                || project.IsTaskOwner(command.TaskItemId, currentUser.UserId);
            if (!hasPermission)
                return Result<TaskItemDTO>.Forbidden("You do not have permission to upload attachment on this task");

            var fileUrl = await fileStorage.UploadAsync(command.Attachment.FileStream, command.Attachment.FileName, command.Attachment.ContentType, ct);

            var task = project.AddTaskAttachment(command.TaskItemId, AttachmentUrl.Create(fileUrl), currentUser.UserId);
            
            project.SetModified(currentUser.UserId.ToString());

            await repository.UpdateAsync(project, ct);

            UserInfo? assignUserInfo = task.AssigneeId.HasValue
                ? await userService.GetUserAsync(task.AssigneeId.Value, ct)
                : null;

            return Result<TaskItemDTO>.Success(task.MapToTaskItemDto(assignUserInfo));
        }
    }
}
