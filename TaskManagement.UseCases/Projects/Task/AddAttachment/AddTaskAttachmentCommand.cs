namespace TaskManagement.UseCases.Projects.Task.AddAttachment
{
    public record AddTaskAttachmentCommand(ProjectId ProjectId, TaskItemId TaskItemId, AttachmentRequestDTO Attachment) : ICommand<Result<TaskItemDTO>>;
}