namespace TaskManagement.UseCases.Projects.Task.Add
{
    public record AddTaskCommand(ProjectId ProjectId, TaskItemTitle Title, string Description) : ICommand<Result<TaskItemDTO>>;
}
