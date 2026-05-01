namespace TaskManagement.UseCases.Projects.Task.Mark
{
    public record MarkTaskCommand(ProjectId ProjectId, TaskItemId TaskItemId, bool IsDone) : ICommand<Result<MarkTaskResultDTO>>;
}