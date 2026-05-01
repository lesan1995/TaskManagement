namespace TaskManagement.UseCases.Projects.Task.AddAttachment
{
    public record AddTaskAttachmentCommand(ProjectId ProjectId, TaskItemId TaskItemId, Stream FileStream, string FileName, string ContentType) : ICommand<Result<TaskItemDTO>>;
}