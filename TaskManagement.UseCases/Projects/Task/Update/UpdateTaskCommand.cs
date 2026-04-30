namespace TaskManagement.UseCases.Projects.Task.Update
{
    public record UpdateTaskCommand(ProjectId ProjectId, TaskItemId TaskItemId, TaskItemTitle Title, string Description) : ICommand<Result<TaskItemDTO>>;
}