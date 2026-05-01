namespace TaskManagement.UseCases.Projects.Task.RemoveAttachment
{
    public record RemoveTaskAttachmentCommand(ProjectId ProjectId, TaskItemId TaskItemId, AttachmentUrl FileUrl) : ICommand<Result<TaskItemDTO>>;
}