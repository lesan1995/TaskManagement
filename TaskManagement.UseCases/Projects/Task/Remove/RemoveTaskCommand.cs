namespace TaskManagement.UseCases.Projects.Task.Remove
{
    public record RemoveTaskCommand(ProjectId ProjectId, TaskItemId TaskItemId) : ICommand<Result>;
}